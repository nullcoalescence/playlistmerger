using System.ComponentModel.DataAnnotations;

namespace playlistmerger.Models
{
    public class ArtistSearch
    {
        [Required(ErrorMessage = "Required")]
        public string Name1 { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Name2 { get; set; }

        [Required(ErrorMessage = "Required")]
        public string PlaylistName { get; set; }
    }
}
