using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShrimpyService : MonoBehaviour
{
    public static ShrimpyService instance;

    [SerializeField]
    string[] symbols;

    [SerializeField]
    string exchangesURL, tickersURL, candlesURL, apiKey, secretKey, tickersString, exchangesString, candlesString;

    [SerializeField]
    Exchanges exchanges;

    [SerializeField]
    int nonce;

    [SerializeField]
    List<Tickers> tickers;

    [SerializeField]
    List<Candles> candles;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetExchanges();
    }

    void GetExchanges()
    {
        StartCoroutine(GetExchangesReq());
    }

    void OnExchangesReceived()
    {
        GetTickers();
        GetVolumes();
    }

    public void GetTickers()
    {
        foreach (Exchange exchange in exchanges.exchanges)
            GetTicker(exchange.exchange);
    }
    
    void GetVolumes()
    {
        foreach (Exchange exchange in exchanges.exchanges)
            foreach (string symbol in symbols)
                GetVolume(exchange.exchange, symbol);
    }

    public void GetTicker(string exchange)
    {
        StartCoroutine(GetTickerReq(exchange));
    }

    public void GetVolume(string exchange, string symbol)
    {
        StartCoroutine(GetCandlesReq(exchange, symbol));
    }

    IEnumerator GetExchangesReq()
    {
        UnityWebRequest www = UnityWebRequest.Get(exchangesURL);
        www.SetRequestHeader("DEV-SHRIMPY-API-KEY", apiKey);
        www.SetRequestHeader("DEV-SHRIMPY-API-NONCE", nonce.ToString());

        nonce++;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(60f);
            StartCoroutine(GetExchangesReq());
        }
        else
        {
            exchangesString = www.downloadHandler.text;
            exchangesString = "{\"exchanges\":" + exchangesString + "}";
            exchanges = JsonUtility.FromJson<Exchanges>(exchangesString);
            OnExchangesReceived();
        }
    }

    IEnumerator GetTickerReq(string exchange)
    {
        UnityWebRequest www = UnityWebRequest.Get(tickersURL + exchange + "/ticker/");
        www.SetRequestHeader("DEV-SHRIMPY-API-KEY", apiKey);
        www.SetRequestHeader("DEV-SHRIMPY-API-NONCE", nonce.ToString());

        nonce++;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(60f);
            StartCoroutine(GetTickerReq(exchange));
        }
        else
        {
            tickersString = www.downloadHandler.text;
            tickersString = "{\"tickers\":" + tickersString + "}";
            Tickers tickersData = JsonUtility.FromJson<Tickers>(tickersString);
            tickersData.exchange = exchange;
            tickers.Add(tickersData);

            StockHolder.onTickerReceived(tickersData);
        }
    }

    IEnumerator GetCandlesReq(string exchange, string symbol)
    {
        UnityWebRequest www = UnityWebRequest.Get(candlesURL + "quoteTradingSymbol=" + "USD" + "&baseTradingSymbol=" + symbol + "&interval=1d");
        www.SetRequestHeader("DEV-SHRIMPY-API-KEY", apiKey);
        www.SetRequestHeader("DEV-SHRIMPY-API-NONCE", nonce.ToString());

        nonce++;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(60f);
            StartCoroutine(GetCandlesReq(exchange, symbol));
        }
        else
        {
            candlesString = www.downloadHandler.text;
            candlesString = "{\"candles\":" + candlesString + "}";
            Candles candlesData = JsonUtility.FromJson<Candles>(candlesString);
            candles.Add(candlesData);
            StockHolder.onCandlesReceived(candlesData, symbol);
        }
    }
}
