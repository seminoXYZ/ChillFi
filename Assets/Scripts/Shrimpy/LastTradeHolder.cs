//LastTradeHolder
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTradeHolder : MonoBehaviour
{
    [SerializeField]
    Text symbol, bidType, price, quantity;
    
    public void InitTradeData(Order order, string symbol, string equivalent)
    {
        this.symbol.text = symbol;
        int bidTypeIndex = UnityEngine.Random.Range(0, 2);

        if (bidTypeIndex == 0)
        {
            int askIndex = 0;
            if (order.asks[0].quantity == quantity.text) askIndex = 1;
            bidType.text = "SELL";
            price.text = order.asks[askIndex].price;

            quantity.text = "$" + (float.Parse(order.asks[askIndex].quantity.Replace(".","")) * float.Parse(order.asks[askIndex].price.Replace(".", "")) * ShrimpyService.instance.btcPrice).ToString().Substring(0,7);
        }
        else
        {
            int bidIndex = 0;
            if (order.bids[0].quantity == quantity.text) bidIndex = 1;
            bidType.text = "BUY";
            price.text = order.bids[bidIndex].price;

            quantity.text = "$" + (float.Parse(order.bids[bidIndex].quantity.Replace(".", "")) * float.Parse(order.bids[bidIndex].price.Replace(".", "")) * ShrimpyService.instance.btcPrice).ToString().Substring(0, 7);
        }
    }
}

