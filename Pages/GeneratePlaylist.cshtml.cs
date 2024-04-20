using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using playlistmerger.Services;

namespace playlistmerger.Pages
{
    public class GeneratePlaylistModel : PageModel
    {
        public string Artist1 { get; set; }
        public string Artist2 { get; set; }
        public string PlaylistName { get; set; }

        public Boolean IsLoading;
        public string GeneratedPlaylistName { get; set; }
        public string PlaylistUrl { get; set; }

        private ILogger<GeneratePlaylistModel> logger;
        private SpotifyService spotifyService;

        public GeneratePlaylistModel(ILogger<GeneratePlaylistModel> logger, SpotifyService spotifyService)
        {
            this.logger = logger;
            this.spotifyService = spotifyService;
            IsLoading = true;
        }

        public async Task OnGetAsync(string artist1, string artist2, string playlistName)
        {
            this.spotifyService.BuildSpotify();

            Artist1 = artist1;
            Artist2 = artist2;
            PlaylistName = playlistName;

            var artist1Songs = await this.spotifyService.SearchTracksAsync(Artist1);
            var artist2Songs = await this.spotifyService.SearchTracksAsync(Artist2);

            var trackUris = new List<string>();
            foreach(var track in artist1Songs)
            {
                trackUris.Add(track.Uri);
            }
            foreach(var track in artist2Songs)
            {
                trackUris.Add(track.Uri);
            }

            var playlist = await this.spotifyService.CreatePlaylist(trackUris, PlaylistName);

            IsLoading = false;
            GeneratedPlaylistName = playlist.Name;
            PlaylistUrl = playlist.Url;
        }

       
    }
}
