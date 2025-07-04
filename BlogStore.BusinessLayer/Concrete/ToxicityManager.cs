using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.BusinessLayer.Abstract;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;


namespace BlogStore.BusinessLayer.Concrete
{
    public class ToxicityManager:IToxicityDetectionService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _huggingFaceApiToken;
        private readonly string? _huggingFaceModelUrl;

        public ToxicityManager(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _huggingFaceApiToken = configuration["HuggingFace:ApiToken"];
            _huggingFaceModelUrl = configuration["HuggingFace:ModelUrl"];

            if (!string.IsNullOrEmpty(_huggingFaceApiToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _huggingFaceApiToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ToxicityDetectionResult> DetecToxicityAsync(string CommentText)
        {
            var requestBody = new { inputs = CommentText };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            if (string.IsNullOrEmpty(_huggingFaceModelUrl))
                return new ToxicityDetectionResult { IsToxic = false, Score = 0, DetectedLabel = "undetected" };

            var response = await _httpClient.PostAsync(_huggingFaceModelUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(responseString);

                // Toksisite modeli yanıtı bekleniyor: [[{label, score}, ...]]
                var modelResponse = TryDeserialize<List<List<ModelPrediction>>>(responseString);
                if (modelResponse != null && modelResponse.Count > 0 && modelResponse[0] != null && modelResponse[0].Count > 0)
                {
                    var topPrediction = modelResponse[0].OrderByDescending(p => p.Score).FirstOrDefault();
                    if (topPrediction != null && !string.IsNullOrEmpty(topPrediction.Label) && topPrediction.Label.ToLower().Contains("toxic"))
                    {
                        return new ToxicityDetectionResult
                        {
                            IsToxic = topPrediction.Score > 0.5,
                            Score = topPrediction.Score,
                            DetectedLabel = topPrediction.Label
                        };
                    }
                    else if (topPrediction != null)
                    {
                        return new ToxicityDetectionResult
                        {
                            IsToxic = false,
                            Score = topPrediction.Score,
                            DetectedLabel = topPrediction.Label
                        };
                    }
                }

                // Çeviri modeli yanıtı bekleniyor: [ { translation_text: ... } ]
                var translationResponse = TryDeserialize<List<TranslationResult>>(responseString);
                if (translationResponse != null && translationResponse.Count > 0)
                {
                    return new ToxicityDetectionResult { IsToxic = false, Score = 0, DetectedLabel = "undetected" };
                }
            }
            return new ToxicityDetectionResult { IsToxic = false, Score = 0, DetectedLabel = "undetected" };
        }

        private class TranslationResult
        {
            public string? translation_text { get; set; }
        }

        private class ModelPrediction
        {
            public string? Label { get; set; }
            public double Score { get; set; }
        }

        // Güvenli deserialize için yardımcı generic metot
        private static T? TryDeserialize<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
