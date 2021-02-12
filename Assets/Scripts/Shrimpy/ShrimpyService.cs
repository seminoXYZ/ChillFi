using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShrimpyService : MonoBehaviour
{
    public static ShrimpyService instance;

    public float orderBookRequestPause = 10f;
    
    public string[] symbols;

    [SerializeField]
    string referenceExchange = "Binance";

    public float btcPrice;

    [SerializeField]
    string exchangesURL, tickersURL, candlesURL, orderbooksURL, apiKey, secretKey, tickersString, exchangesString, candlesString, orderbookString;

    [SerializeField]
    GameObject lastTradePrefab, content;
    
    [SerializeField]
    int nonce;
    
    [SerializeField]
    Orderbooks orderbooks;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetLastTrade();
    }
    
    void OnDisable()
    {
        Debug.LogWarning("Shrimpy has been rebooted!");
    }

    void OnExchangesReceived()
    {
        Invoke("GetLastTrade", 2f);
    }

    void GetLastTrade()
    {
        StartCoroutine(GetOrderbookReq(referenceExchange, symbols[Random.Range(0, symbols.Length)]));
    }
    
    public IEnumerator GetOrderbookReq(string exchange, string symbol)
    {
        UnityWebRequest www = UnityWebRequest.Get(orderbooksURL + exchange);
        www.SetRequestHeader("DEV-SHRIMPY-API-KEY", apiKey);
        www.SetRequestHeader("DEV-SHRIMPY-API-NONCE", nonce.ToString());

        nonce++;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Invoke("GetLastTrade", 2f);
        }
        else
        {
            orderbookString = www.downloadHandler.text;
            orderbookString = "{\"orderbooks\":" + orderbookString + "}";
            Orderbooks orderBooksData = JsonUtility.FromJson<Orderbooks>(orderbookString);
            orderbooks = orderBooksData;
            GetSymbolTrade(orderBooksData, symbol);
        }
    }

    void GetSymbolTrade(Orderbooks orderBooksData, string symbol)
    {
        foreach(Orderbook orderBook in orderBooksData.orderbooks)
        {
            if (orderBook.baseSymbol == symbol && orderBook.quoteSymbol == "BTC")
            {
                LastTradeHolder lastTradeHolder = Instantiate(lastTradePrefab, content.transform).GetComponent<LastTradeHolder>();
                RemoveTheTopTrade();
                lastTradeHolder.InitTradeData(orderBook.orderBooks[0].orderBook, symbol, "BTC");
                Invoke("GetLastTrade", 2f);
                return;
            }
        }

        Invoke("GetLastTrade", 2f);
    }

    void RemoveTheTopTrade()
    {
        if(content.transform.childCount > 6)
            Destroy(content.transform.GetChild(0).gameObject);
    }
}
