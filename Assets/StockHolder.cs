using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockHolder : MonoBehaviour
{
    [SerializeField]
    string symbol;
    [SerializeField]
    Text position, price, volume, change;

    float volumeValue;
    
    public delegate void OnTickerReceived(Tickers tickers);
    public static OnTickerReceived onTickerReceived;

    public delegate void OnCandlesReceived(Candles candles, string symbol);
    public static OnCandlesReceived onCandlesReceived;

    public delegate void OnListingsReceived(ListingsLatest listingsLatest);
    public static OnListingsReceived onListingsReceived;

    void Start()
    {
        onTickerReceived += OnTicker;
        onCandlesReceived += OnCandles;
        onListingsReceived += OnListings;
    }

    private void OnListings(ListingsLatest listingsLatest)
    {
        foreach(ListingData data in listingsLatest.listings.data)
        {
            if(data.symbol == symbol)
            {
                position.text = symbol;

                price.text = "$" + data.quote.USD.price;
                volume.text = GetFormattedVolume((ulong)data.quote.USD.volume_24h);
                change.text = data.quote.USD.percent_change_24h.ToString().Substring(0,7) + "%";
            }
        }
    }

    IEnumerator RequestTicker(string exchange)
    {
        yield return new WaitForSeconds(60f);
        ShrimpyService.instance.GetTicker(exchange);
        yield return true;
    }
    
    void OnTicker(Tickers tickers)
    {
        foreach(Ticker ticker in tickers.tickers)
        if(ticker.symbol == this.symbol)
        {
            position.text = ticker.symbol;
            price.text = "$" + ticker.priceUsd.ToString();
            change.text = ((ticker.percentChange24hUsd).ToString()).Substring(0,5) + "%";
            StartCoroutine(RequestTicker(tickers.exchange));
        }
    }

    private void OnCandles(Candles candles, string symbol)
    {
        if (symbol == this.symbol)
        {
            if (candles.candles.Length == 0)
            {
                //Debug.Log("NULL");
                return;
            }

            string rawVolume = candles.candles[candles.candles.Length - 1].volume;

            if(rawVolume != "")
            {
                //Debug.Log(rawVolume + " for " + symbol);
                string optimizedVolume = rawVolume.Substring(0, rawVolume.IndexOf("."));

                volumeValue = volumeValue + (int.Parse(optimizedVolume) / 1000);

                volume.text = volumeValue.ToString() + "K";
            } 
        }  
    }

    string GetFormattedVolume(ulong volume)
    {
        string volumeString = "";

        if (volume < 1000)
            volumeString = volume.ToString();

        if (volume > 999 && volume < 1000000)
            volumeString = (volume / 1000f).ToString().Substring(0, 7) + "K";

        if (volume > 999999 && volume < 1000000000)
            volumeString = (volume / 1000000f).ToString().Substring(0, 7) + "M";

        if (volume > 999999999)
            volumeString = (volume / 1000000000f).ToString().Substring(0, 7) + "B";

        return volumeString;
    }
}
