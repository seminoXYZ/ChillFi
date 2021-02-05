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
    
    [SerializeField]
    string exchange;
    
    public delegate void OnOrderBooksReceived(Orderbooks candles);
    public static OnOrderBooksReceived onOrderBooksReceived;

    void Start()
    {
        onOrderBooksReceived += OnOrderBook;
        ShrimpyService.instance.StartCoroutine(ShrimpyService.instance.GetOrderbookReq(exchange));
    }
    
    private void OnOrderBook(Orderbooks orderBooks)
    {
        if (orderBooks.orderbooks[0].orderBooks[0].exchange == exchange)
        {
            symbol.text = orderBooks.orderbooks[0].baseSymbol;
            int bidTypeIndex = UnityEngine.Random.Range(0, 2);

            if (bidTypeIndex == 0)
            {
                bidType.text = "SELL";
                price.text = orderBooks.orderbooks[0].orderBooks[0].orderBook.asks[0].price;
                quantity.text = orderBooks.orderbooks[0].orderBooks[0].orderBook.asks[0].quantity;
            }   
            else
            {
                bidType.text = "BUY";
                price.text = orderBooks.orderbooks[0].orderBooks[0].orderBook.bids[0].price;
                quantity.text = orderBooks.orderbooks[0].orderBooks[0].orderBook.bids[0].quantity;
            }

            Invoke("GetOrderBooksWithDelay", ShrimpyService.instance.orderBookRequestPause);
        }
    }

    void GetOrderBooksWithDelay()
    {
        ShrimpyService.instance.StartCoroutine(ShrimpyService.instance.GetOrderbookReq(exchange));
    }
}

