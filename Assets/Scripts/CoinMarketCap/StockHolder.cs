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

    public delegate void OnHoldingsReceived(Holdings holdings);
    public static OnHoldingsReceived onHoldingsReceived;

    
    void Start()
    {
        symbol = ShrimpyService.instance.symbols[transform.GetSiblingIndex()];
        onListingsReceived += OnListings;
        onHoldingsReceived += OnHoldings;
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

                price.text = "$" + StringFormatter.GetAmericanFormat(StringFormatter.FormatIntoTwoDecimalValue(data.quote.USD.price)).ToString();
                volume.text = StringFormatter.GetTwoDigitsValueWithMultiplier((ulong)data.quote.USD.volume_24h, 7);
                if(data.quote.USD.percent_change_24h.ToString().Length > 7)
                    change.text = StringFormatter.FormatIntoTwoDecimalValue(data.quote.USD.percent_change_24h) + "%";
                else
                    change.text = StringFormatter.FormatIntoTwoDecimalValue(data.quote.USD.percent_change_24h) + "%";
            }
        }
    }

    private void OnHoldings(Holdings holdings)
    {
        foreach(Holding holding in holdings.holdings)
        {
            if(holding.Currency == symbol)
            {
                float priceValue = float.Parse(holding.Prices.Replace("$",""));

                if (symbol == "BTC")
                    ShrimpyService.instance.btcPrice = priceValue;

                position.text = symbol;

                price.text = "$" + StringFormatter.GetAmericanFormat(priceValue).ToString();
                //volume.text = StringFormatter.GetTwoDigitsValueWithMultiplier((ulong)data.quote.USD.volume_24h, 7);
                change.text = StringFormatter.FormatIntoTwoDecimalValue(holding.Percent_Holding * 100) + "%";
            }
        }
    }
}
