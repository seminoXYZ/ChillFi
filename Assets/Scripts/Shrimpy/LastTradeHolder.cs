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

    [SerializeField]
    float lifeTimeLimit = 30f;

    [SerializeField]
    float lifeTime;
    
    void Start()
    {
        StartCoroutine(LifeTimeCounter());
    }

    IEnumerator LifeTimeCounter()
    {
        while(lifeTime < lifeTimeLimit)
        {
            lifeTime += Time.deltaTime;
            yield return null;
        }
            
        Debug.LogWarning("LifeTime Limit has been reached!");
        ShrimpyService.instance.enabled = false;
        ShrimpyService.instance.enabled = true;

        yield return true;
    }

    public void InitTradeData(Order order, string symbol, string equivalent)
    {
        this.symbol.text = symbol;
        int bidTypeIndex = UnityEngine.Random.Range(0, 2);
        
        if (bidTypeIndex == 0)
        {
            int askIndex = 0;
            if (order.asks[0].quantity == cost.text) askIndex = 1;
            bidType.text = "SELL";
            quantity.text = StringFormatter.GetTwoDigitsValueWithMultiplier(float.Parse(order.asks[askIndex].quantity), 7);
            if (quantity.text == "0.00" || quantity.text == "0")
                quantity.text = "0.01";

            float costString = StringFormatter.FormatIntoTwoDecimalValue(float.Parse(order.asks[askIndex].quantity) * float.Parse(order.asks[askIndex].price) * ShrimpyService.instance.btcPrice);
            
            cost.text = "$" + StringFormatter.GetAmericanFormat(costString);
        }
        else
        {
            int bidIndex = 0;
            if (order.bids[0].quantity == cost.text) bidIndex = 1;
            bidType.text = "BUY";
            quantity.text = StringFormatter.GetTwoDigitsValueWithMultiplier(float.Parse(order.bids[bidIndex].quantity), 7);
            if (quantity.text == "0.00")
                quantity.text = "0.01";

            float costString = StringFormatter.FormatIntoTwoDecimalValue(float.Parse(order.bids[bidIndex].quantity) * float.Parse(order.bids[bidIndex].price) * ShrimpyService.instance.btcPrice);
            
            cost.text = "$" + StringFormatter.GetAmericanFormat(costString).ToString();
        }
    }
}

