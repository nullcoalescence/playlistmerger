using Microsoft.AspNetCore.Mvc.RazorPages;
using playlistmerger.Services;

namespace playlistmerger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SpotifyClientBuilderService spotifyClientBuilderService;

        private const int LIMIT = 10;

        public string Username { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SpotifyClientBuilderService spotifyClientBuilderService)
        {
            _logger = logger;
            this.spotifyClientBuilderService = spotifyClientBuilderService;
        }

        public async Task OnGet()
        {
            var spotify = await this.spotifyClientBuilderService.BuildSpotifyClient();

            var profile = await spotify.UserProfile.Current();

            Username = profile.DisplayName.ToString();

        }
    }
}
