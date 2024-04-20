using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using playlistmerger.Dto;
using playlistmerger.Models;
using playlistmerger.Services;

namespace playlistmerger.Pages
{
    public class CreatePlaylistModel : PageModel
    {
        [BindProperty]
        public ArtistSearch? ArtistSearch { get; set; }

        public List<ArtistDto> ArtistResults1 { get; set; } = new List<ArtistDto>();
        

        private readonly ILogger<CreatePlaylistModel> logger;
        private SpotifyService spotifyService;

        public CreatePlaylistModel(ILogger<CreatePlaylistModel> logger, SpotifyService spotifyService)
        {
            this.logger = logger;
            this.spotifyService = spotifyService;
        }

        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await this.spotifyService.BuildSpotify();
            ArtistResults1 = await this.spotifyService.SearchArtists(ArtistSearch.Name1);
            

            return Page();
        }
    }
}
