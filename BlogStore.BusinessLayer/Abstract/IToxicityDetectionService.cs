using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IToxicityDetectionService
    {
        Task<ToxicityDetectionResult> DetecToxicityAsync(string CommentText);
    }

    public class ToxicityDetectionResult
    {
        public bool IsToxic { get; set; }
        public double Score { get; set; }
        public string DetectedLabel { get; set; }
    }
}
