using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiClient.Model
{
    public class Pokemon
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public Sprites Sprites { get; set; }
    }

    public class Sprites
    {
        public Other Other { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string Front_Default { get; set; }
    }

    public class Other
    {
        [JsonPropertyName("official-artwork")] // Map "official-artwork" to OfficialArtwork
        public OfficialArtwork OfficialArtwork { get; set; }
    }


}
