using Microsoft.AspNetCore.Authentication;
using SpotifyAPI.Web;

namespace playlistmerger.Services
{
    public class SpotifyClientBuilderService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SpotifyClientConfig spotifyClientConfig;

        public SpotifyClientBuilderService(IHttpContextAccessor httpContextAccessor, SpotifyClientConfig spotifyClientConfig)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.spotifyClientConfig = spotifyClientConfig;
        }

        public async Task<SpotifyClient> BuildSpotifyClient()
        {
            var token = await this.httpContextAccessor.HttpContext.GetTokenAsync("Spotify", "access_token");

            if (token == null)
            {
                throw new Exception("Could not build Spotify client: error fetching Spotify auth token from HttpContext");
            }

            return new SpotifyClient(this.spotifyClientConfig.WithToken(token));
        }
    }
}
