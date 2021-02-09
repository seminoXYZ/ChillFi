# ChillFi
 
 ## Introduction
 
 The project completed using **Unity 2020.2.1f1**. Please, make sure to open it in a proper Unity version, because reimport for later versions break the project's workability.
 To retrieve the data from DeFi aggregators we use **Shrimpy** and **CoinMarketCap** APIs.
 
 Target project platform is **Standalone** (Windows and OSX), however that's possible to compile it for mobile platforms if needed.
 
 Project purpose is a procedural generation of a hypotetical trader room for YouTube stream with DeFi aggregator live data displaying at two monitors and predefined Spotify music playlist that should play during the stream.
 **ChillFi** app generates visual part of the stream, **Spotify** plays the music (so we don't need any sounds comes from the app itself) and **OBS Studio** (or it's analog) launched in background to record and stream the gathered video/audio procedural simulation to YouTube.
 
 **WARNING!**
 To get the proper Unity version you can get it from an archive page here: https://unity3d.com/ru/get-unity/download/archive
 
 ## Quick Start Guide
 
 Once you have the project opened you can test it by press **Play** button at the top.
 
 At start Unity will forward you to your browser, where it will ask you to login into your Spotify account.
 
 That happens because of **Spotify4Unity** plugin that had been integrated to display current track that have been playing through **Spotify** during the streaming session.

### Disable Spotify AutoConnect
For development and debug purposes you can disable this **AutoConnect** feature to not spend time to close **Spotify** Login page each time you enter Unity **PlayMode**.
You can disable it in **SpotifyService** MonoBehaviour class at **SpotifyService** gameObject.
![alt text](https://github.com/ssemino/ChillFi/blob/main/Assets/Resources/README/readme01.png)

