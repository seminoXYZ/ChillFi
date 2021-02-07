//LastTradeHolder
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTradeHolder : MonoBehaviour
{
    [SerializeField]
    Text symbol, bidType, quantity, cost;
    
    public void InitTradeData(Order order, string symbol, string equivalent)
    {
        this.symbol.text = symbol;
        int bidTypeIndex = UnityEngine.Random.Range(0, 2);
        
        if (bidTypeIndex == 0)
        {
            int askIndex = 0;
            if (order.asks[0].quantity == cost.text) askIndex = 1;
            bidType.text = "SELL";
            Debug.Log(order.asks[askIndex].quantity);
            Debug.Log(StringFormatter.GetCorrectFloat(order.asks[askIndex].quantity));
            quantity.text = StringFormatter.GetTwoDigitsValueWithMultiplier(float.Parse(order.asks[askIndex].quantity.Replace(".", ",")), 7);
            if (quantity.text == "0.00")
                quantity.text = "0.01";

            string costString = StringFormatter.FormatIntoTwoDecimalValue((StringFormatter.GetCorrectFloat(order.asks[askIndex].quantity) * StringFormatter.GetCorrectFloat(order.asks[askIndex].price) * ShrimpyService.instance.btcPrice).ToString());
            Debug.Log(costString);
            cost.text = "$" + StringFormatter.GetAmericanFormat(costString);
        }
        else
        {
            int bidIndex = 0;
            if (order.bids[0].quantity == cost.text) bidIndex = 1;
            bidType.text = "BUY";
            Debug.Log(StringFormatter.GetCorrectFloat(order.bids[bidIndex].quantity));
            quantity.text = StringFormatter.GetTwoDigitsValueWithMultiplier(float.Parse(order.bids[bidIndex].quantity.Replace(".", ",")), 7);
            if (quantity.text == "0.00")
                quantity.text = "0.01";

            string costString = StringFormatter.FormatIntoTwoDecimalValue((StringFormatter.GetCorrectFloat(order.bids[bidIndex].quantity) * StringFormatter.GetCorrectFloat(order.bids[bidIndex].price) * ShrimpyService.instance.btcPrice).ToString());
            Debug.Log(costString);
            cost.text = "$" + StringFormatter.GetAmericanFormat(costString);
        }
    }
}

