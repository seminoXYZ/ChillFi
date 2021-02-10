
![alt text](https://github.com/ssemino/ChillFi/blob/main/Assets/Resources/README/readme00.png)
 
 ## Introduction
 
ChillFi is the love child between Chill #LoFi beats and #DeFi trading. It streams 24/7 to Youtube, displays live cryptocurrency exchange data, and plays a copyright-free Spotify music playlist.

Watch it live here: https://chillfi.live

### Necessary Software

**ChillFi** game application to generate visuals including monitors

**Spotify** to play music in your stream (no sound comes from the actual ChillFi game)

**OBS Streamlabs** to gather your video/audio and stream to Youtube.

 ### Key Details
 
 If you want to copy this project you must use **Unity 2020.2.1f1**. Failing to do so will result in reimport for later versions to break the project's functionality.
 To retrieve the cryptocurrency pricing data we use **Shrimpy** and **CoinMarketCap** APIs.
 
 Target project platform is **Standalone** (Windows and OSX), however it's possible to compile it for mobile platforms if needed.
 
 **WARNING!**
 To get the proper version of Unity go to the archive page here: https://unity3d.com/ru/get-unity/download/archive
 
 ## Quick Start Guide
 
 Once you have the project opened on Unity you can test it by pressing the **Play** button at the top.
 
 At first, Unity will forward you to your browser, where it will ask you to login into your Spotify account.
 
 That happens because of the **Spotify4Unity** plugin that has been integrated to display the current track you are playing on **Spotify** during the streaming session.

### Disable Spotify AutoConnect
For development and debug purposes you can disable this **AutoConnect** feature to not spend time to close **Spotify** Login page each time you enter Unity **PlayMode**.
You can disable it in **SpotifyService** MonoBehaviour class at **SpotifyService** gameObject.

![alt text](https://github.com/ssemino/ChillFi/blob/main/Assets/Resources/README/readme01.png)

### DeFi Ticker Setup

If you want to change the current list of tickers on the left monitor ( streamed from **CoinMarketCap** API), you need to change the ticker to an existing one in **ShrimpyService** MonoBehaviour class at **ShrimpyService** gameObject. You can also change the order of the symbols in this list.

![alt text](https://github.com/ssemino/ChillFi/blob/main/Assets/Resources/README/readme02.png)

**WARNING!** Make sure to choose a proper ticker in the list. Incorrect or not covered tickers in **CoinMarketCap** will result in an error.

### DeFi API Workflow

To get live cryptocurrency ticker data for our application we use: 

**Shrimpy API** (https://developers.shrimpy.io/docs/) 

**CoinMarketCap API** (https://coinmarketcap.com/api/documentation/v1/).

Shrimpy is used to receive data for the 'right' monitor currently titled "Latest Trades". This data comes with limitations in the **Shrimpy** service policy. We only have 60 request per minute for one authenticated IP (that sends API-KEY request header), or, 10 requests per minute without an API-KEY.

**Shrimpy** pulls in an OrderBook with live bids and asks. The **ShrimpyService** class then selects bids and asks at random, choosing only the trades for tickers in our defined list on the 'left' monitor. 
