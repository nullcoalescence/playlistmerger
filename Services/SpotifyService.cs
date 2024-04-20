using playlistmerger.Dto;
using SpotifyAPI.Web;

namespace playlistmerger.Services
{
    public class SpotifyService
    {
        private readonly ILogger<SpotifyService> logger;
        private readonly SpotifyClientBuilderService spotifyBuilder;

        private SpotifyClient spotify;

        public SpotifyService(ILogger<SpotifyService> logger, SpotifyClientBuilderService spotifyBuilder)
        {
            this.logger = logger;
            this.spotifyBuilder = spotifyBuilder;
        }

        public async Task BuildSpotify()
        {
            this.spotify = await this.spotifyBuilder.BuildSpotifyClient();
        }

        public async Task<List<ArtistDto>> SearchArtists(string artistName)
        {
            if (this.spotify == null) throw new Exception("SpotifyClient not built");

            var search = await this.spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, artistName));

            var iter = 1;
            var maxResults = 5;
            var results = new List<ArtistDto>();

            await foreach(var item in this.spotify.Paginate(search.Artists, (s) => s.Artists))
            {
                results.Add(new ArtistDto
                {
                    Id = item.Id,
                    ArtistName = item.Name,
                    ImageUrl = item.Images.First().Url
                });

                if (results.Count > maxResults) break;
            }

            return results;
        }


    }
}
