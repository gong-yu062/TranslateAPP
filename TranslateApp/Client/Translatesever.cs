using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TranslateApp
{

    public class TranslationInputModel
    {
        [Required]
        public string TextToTranslate { get; set; } = string.Empty;

        [Required]
        public string TargetLanguage { get; set; } = string.Empty;
    }

    public class TranslationResult
    {
        public Data Data { get; set; } = new Data();
        public string TranslatedData { get; set; } = string.Empty;
    }

    public class Data
    {
        public Translation[] Translations { get; set; } = Array.Empty<Translation>();
    }

    public class Translation
    {
        public string Translatedlanguage { get; set; } = string.Empty;
        public string TranslatedText { get; set; } = string.Empty;
    }

    public class TranslationResponse
    {
        public string TranslatedText { get; set; }

        public TranslationResponse()
        {
            TranslatedText = string.Empty;
        }
    }

    public class TranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "e2d746bb27mshfd7a9553bd4b209p151d10jsn9a5efdad6524"; // Replace with your actual API key
        private readonly string _apiUrl = "https://google-translate113.p.rapidapi.com/api/v1/translator/detect-language";

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "google-translate113.p.rapidapi.com");
        }


        public async Task<string> TranslateTextAsync(string text, string targetLanguage)
        {
            var requestBody = new Dictionary<string, string>
            {
                { "text", text },
                { "target_lang", targetLanguage }
            };
            var content = new FormUrlEncodedContent(requestBody);
            
            var response = await _httpClient.PostAsync(_apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var translationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonResponse);
                if (translationResult != null)
                {
                    return translationResult.TranslatedData;
                }
                else
                {
                    return "Error in translation";
                }
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            // Add this line to return a value when the status code is not successful
            return "Error in request";
        }

    }

}







