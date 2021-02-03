using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Ticker
{
    public string symbol;
    public double last;
    public int volume;
}

public class StockHolder : MonoBehaviour
{
    [SerializeField]
    string ticker;
    [SerializeField]
    Text bid, price, volume, floatShort;

    public delegate void OnTickerReceived(Ticker ticker);
    public static OnTickerReceived onTickerReceived;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("RequestTicker", Random.Range(0f, 2f));
    }

    void RequestTicker()
    {
        onTickerReceived += OnTicker;
        PolygonRESTService.instance.GetTicker(ticker);
    }

    void OnTicker(Ticker ticker)
    {
        if(ticker.symbol == this.ticker)
        {
            bid.text = ticker.symbol;
            price.text = "$" + ticker.last.ToString();
            volume.text = ((ticker.volume / 1000000f).ToString()).Substring(0,4) + "M";
            floatShort.text = Random.Range(100, 121).ToString() + " %";
            Invoke("RequestTicker", 10f);
        }
    }
}
