# playlistmerger

## config
install and setup environment with dotnet-secrets. you'll need to register an app as a spotify dev and get the client id & secret.
```
git clone https://github.com/nullcoalescence/playlistmerger	~/playlistmerger
dotnet user-secrets init
dotnet user-secrets set 'SpotifyApi:ClientId' 'your_client_id_here'
dotnet user-secrets set 'SpotifyApi:ClientSecret' 'your_client_secret_here'
```

## todo
- better artist search ui/ux
- more playlist creation options
	- more than 2 artists
	- increase length of playlist
	- include/not include/only include liked songs from selected artists
- set playlist description
- about page
- randomizer