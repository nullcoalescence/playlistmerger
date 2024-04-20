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

        public async Task<List<ArtistDto>> SearchArtistsAsync(string artistName)
        {
            if (this.spotify == null) throw new Exception("SpotifyClient not built");

            var search = await this.spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, artistName));

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

        public async Task<List<TrackDto>> SearchTracksAsync(string artistName)
        {
            if (this.spotify == null) throw new Exception("SpotifyClient not built");

            var searchQuery = $"artist:{artistName}";

            var search = await this.spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, searchQuery));

            var maxResults = 20;
            var results = new List<TrackDto>();

            await foreach(var item in this.spotify.Paginate(search.Tracks, (s) => s.Tracks))
            {
                results.Add(new TrackDto
                {
                    Id = item.Id,
                    TrackName = item.Name,
                    Uri = item.Uri
                });

                if (results.Count > maxResults) break;
            }

            return results;
        }

        public async Task<PlaylistDto> CreatePlaylist(List<string> trackUris, string name)
        {
            if (this.spotify == null) throw new Exception("SpotifyClient not built");

            var randomized = RandomizeList(trackUris);

            var user = await this.spotify.UserProfile.Current();

            var playlist = await this.spotify.Playlists.Create(
                user.Id,
                new PlaylistCreateRequest(name));

            var paginatedUris = PaginateUris(randomized);

            foreach(var list in paginatedUris)
            {
                await this.spotify.Playlists.AddItems(
                    playlist.Id,
                    new PlaylistAddItemsRequest(list));
            }

            return new PlaylistDto { Id = playlist.Id, Name = playlist.Name, Url = playlist.Uri };
        }

        // Paginate a list of tracks so they can be added to a playlist
        // Spotify API's playlist endpoint only allows 100 songs at once
        private List<List<string>> PaginateUris(List<string> uris)
        {
            var pageSize = 100;
            var outer = new List<List<string>>();

            if (uris.Count < pageSize)
            {
                outer.Add(uris);
                return outer;
            }

            var div = Math.DivRem(uris.Count, pageSize);

            // Paginate
            for (int i = 0; i <= div.Quotient; i++)
            {
                var listTemp = (uris
                    .Skip(i * pageSize)
                    .Take(pageSize)
                    .ToList());

                outer.Add(listTemp);
            }

            // Any remaining after last 100
            outer.Add(uris.Skip(uris.Count - div.Remainder).ToList());

            return outer;
        }

        private List<string> RandomizeList(List<string> list)
        {
            List<string> randomizedList = new List<string>();
            Random rand = new Random();
            while (list.Count > 0)
            {
                int index = rand.Next(0, list.Count);
                randomizedList.Add(list[index]);
                list.RemoveAt(index);
            }

            return randomizedList;
        }

    }
}
