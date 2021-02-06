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
    
    public delegate void OnListingsReceived(ListingsLatest listingsLatest);
    public static OnListingsReceived onListingsReceived;
    
    void Start()
    {
        symbol = ShrimpyService.instance.symbols[transform.GetSiblingIndex()];
        onListingsReceived += OnListings;
    }

    private void OnListings(ListingsLatest listingsLatest)
    {
        foreach(ListingData data in listingsLatest.listings.data)
        {
            if(data.symbol == symbol)
            {
                if (symbol == "BTC")
                    ShrimpyService.instance.btcPrice = data.quote.USD.price;

                position.text = symbol;

                if(price.text != "$" + data.quote.USD.price)
                    Debug.Log(symbol + " price changed to : " + data.quote.USD.price);

                price.text = "$" + data.quote.USD.price;
                volume.text = GetFormattedVolume((ulong)data.quote.USD.volume_24h);
                if(data.quote.USD.percent_change_24h.ToString().Length > 7)
                    change.text = data.quote.USD.percent_change_24h.ToString().Substring(0,7) + "%";
                else
                    change.text = data.quote.USD.percent_change_24h.ToString() + "%";
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
