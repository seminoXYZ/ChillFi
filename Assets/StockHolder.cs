using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Ticker
{
    public string name;
    public string symbol;
    public float priceUsd;
    public float percentChange24hUsd;
}

public class StockHolder : MonoBehaviour
{
    [SerializeField]
    string symbol;
    [SerializeField]
    Text position, price, volume, floatShort;

    public delegate void OnTickerReceived(Tickers tickers);
    public static OnTickerReceived onTickerReceived;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("RequestTicker", Random.Range(0f, 2f));
    }

    void RequestTicker()
    {
        onTickerReceived += OnTicker;
        ShrimpyService.instance.GetTicker(symbol);
    }

    void OnTicker(Tickers tickers)
    {
        foreach(Ticker ticker in tickers.tickers)
        if(ticker.symbol == this.symbol)
        {
            position.text = ticker.symbol;
            price.text = "$" + ticker.priceUsd.ToString();
            Debug.Log(ticker.priceUsd);
            volume.text = ((ticker.percentChange24hUsd).ToString());
            Debug.Log(ticker.percentChange24hUsd);
            floatShort.text = Random.Range(100, 121).ToString() + " %";
            Invoke("RequestTicker", 60f);
        }
    }
}
