using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            if (string.IsNullOrEmpty(title))
                return string.Empty;

            // Türkçe karakterleri değiştir
            title = title.Replace("ı", "i")
                        .Replace("ğ", "g")
                        .Replace("ü", "u")
                        .Replace("ş", "s")
                        .Replace("ö", "o")
                        .Replace("ç", "c")
                        .Replace("İ", "i")
                        .Replace("Ğ", "g")
                        .Replace("Ü", "u")
                        .Replace("Ş", "s")
                        .Replace("Ö", "o")
                        .Replace("Ç", "c");

            // Küçük harfe çevir
            title = title.ToLowerInvariant();

            // Sadece harf, rakam ve boşluk bırak
            title = Regex.Replace(title, @"[^a-z0-9\s-]", "");

            // Birden fazla boşluğu tek boşluk yap
            title = Regex.Replace(title, @"\s+", " ").Trim();

            // Boşlukları tire ile değiştir
            title = Regex.Replace(title, @"\s", "-");

            return title;
        }
    }
}
