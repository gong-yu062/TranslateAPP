using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TranslateApp
{
    // This class represents the input model for the translation service. It contains the text to translate and the target language.
    public class TranslationInputModel
    {
        // The text to translate. This field is required.
        [Required]
        public string TextToTranslate { get; set; } = string.Empty;

        // The target language for the translation. This field is required.
        [Required]
        public string TargetLanguage { get; set; } = string.Empty;
    }

    // This class represents the result of a translation.
    public class TranslationResult
    {
        // The translated text.
        public string? Trans { get; set; }
    }

    // This class represents the data in the response from the translation API.
    public class Translation
    {
        // The language to which the text was translated.
        public string Translatedlanguage { get; set; } = string.Empty;

        // The translated text.
        public string TranslatedText { get; set; } = string.Empty;
    }

    // This class represents the response from the translation service.
    public class TranslationResponse
    {
        // The translated text.
        public string TranslatedText { get; set; }

        // Constructor that initializes the TranslatedText field to an empty string.
        public TranslationResponse()
        {
            TranslatedText = string.Empty;
        }
    }

    public class TranslationService
    {
        private readonly HttpClient httpClient;  // HttpClient used to send HTTP requests
        private readonly string apiKey = "e2d746bb27mshfd7a9553bd4b209p151d10jsn9a5efdad6524"; // API key for the translation service
        private readonly string apiUrl = "https://google-translate113.p.rapidapi.com/api/v1/translator/text";

        public TranslationService(HttpClient httpClient)
        {
            // Initialize HttpClient and set default request headers
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
            this.httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "google-translate113.p.rapidapi.com");
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
                    { "to", toLanguage },
                };
            var content = new FormUrlEncodedContent(requestBody);
            var response = await httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON response: {jsonResponse}");
                var translationResult = JsonConvert.DeserializeObject<TranslationResult>(jsonResponse);
                Console.WriteLine($"Deserialized translation result: {translationResult}");
                if (translationResult != null)
                {
                    return translationResult.Trans;
                }
                else
                {
                    return "Error in translation";
                }
            }
            else
            {
                // If the request is not successful, log the status code and response body
                Console.WriteLine($"Status code: {response.StatusCode}");
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response body: {responseBody}");
            }

            return "Error in request";
        }
    }
}