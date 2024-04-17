using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using playlistmerger.Services;
using SpotifyAPI.Web;

namespace playlistmerger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SpotifyClientBuilderService spotifyClientBuilderService;

        private const int LIMIT = 10;

        public string Username { get; set; }

        public string Next { get; set; }
        public string Previous { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SpotifyClientBuilderService spotifyClientBuilderService)
        {
            _logger = logger;
            this.spotifyClientBuilderService = spotifyClientBuilderService;
        }

        public async void OnGet()
        {
            var spotify = await this.spotifyClientBuilderService.BuildSpotifyClient();

            var profile = await spotify.UserProfile.Current();
            Username = profile.DisplayName.ToString();
        }
    }
}
