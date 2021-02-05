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
    
    void Start()
    {
        onTickerReceived += OnTicker;
        onCandlesReceived += OnCandles;
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
}
