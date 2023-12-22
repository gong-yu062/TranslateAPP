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
        public string? Trans { get; set; }
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
        private readonly string _apiUrl = "https://google-translate113.p.rapidapi.com/api/v1/translator/text";

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "google-translate113.p.rapidapi.com");
        }

        public async Task<string> TranslateTextAsync(string text, string fromLanguage, string toLanguage)
        {
            // Check if the text or the target language is null or empty
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(fromLanguage) || string.IsNullOrEmpty(toLanguage))
            {
                return "Text, from language or to language is null or empty";
            }

            var requestBody = new Dictionary<string, string>
                {
                    { "text", text },
                    { "from", fromLanguage },
                    { "to", toLanguage }
                };
            var content = new FormUrlEncodedContent(requestBody);

            var response = await _httpClient.PostAsync(_apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON response: {jsonResponse}");

                var translationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonResponse);
                Console.WriteLine($"Deserialized translation result: {translationResult}");
                if (translationResult != null)
                {
                    //Console.WriteLine($"Deserialized translation result: {translationResult}");
                    return translationResult.Trans;
                }
                else
                {
                    return "Error in translation";
                }
                }
                else
                {
                    Console.WriteLine($"Status code: {response.StatusCode}");
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response body: {responseBody}");
                }

            return "Error in request";
        }

    }

}







