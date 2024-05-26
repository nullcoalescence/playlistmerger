# breadhinge

## about
```breadhinge``` is an app that lets you generate spotify playlists containing songs from 2 or more artists. You ever queue up some TOOL and realize that some Gucci Mane would soung good alongside it? Well now you can quickly generate a playlist, instead of going to each of those artists pages and intermixing your queue with songs from both artists before realizing you've wasted 10 minutes sitting in the car and the traffic home from work is getting worse and worse and you are just putting off the inevitable...

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
