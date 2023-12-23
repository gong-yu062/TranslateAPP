using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TranslateApp
{

    // This class represents the data in the response from the translation API.
    public class Data
    {
        // An array of translations.
        public Translation[] Translations { get; set; } = Array.Empty<Translation>();
    }
}