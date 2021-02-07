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

                //if(price.text != "$" + data.quote.USD.price)
                //    Debug.Log(symbol + " price changed to : " + data.quote.USD.price);

                price.text = "$" + StringFormatter.GetAmericanFormat(StringFormatter.FormatIntoTwoDecimalValue(data.quote.USD.price.ToString()));
                volume.text = StringFormatter.GetTwoDigitsValueWithMultiplier((ulong)data.quote.USD.volume_24h, 7);
                if(data.quote.USD.percent_change_24h.ToString().Length > 7)
                    change.text = StringFormatter.FormatIntoTwoDecimalValue(data.quote.USD.percent_change_24h.ToString().Substring(0, 7)).Replace(",",".") + "%";
                else
                    change.text = StringFormatter.FormatIntoTwoDecimalValue(data.quote.USD.percent_change_24h.ToString()).Replace(",", ".") + "%";
            }
        }
    }
}
