using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChillFiAPIHandler : MonoBehaviour
{
    [SerializeField]
    string holdingsURL, latestTradesURL;
    [SerializeField]
    string holdingsString;
    [SerializeField]
    Holdings holdings;
    [SerializeField]
    string latestTradesString;
    [SerializeField]
    LatestTrades latestTrades;
    [SerializeField]
    float delayBetweenHoldings = 1;
    [SerializeField]
    float delayBetweenLatestTrades = 1;
    
    [SerializeField]
    GameObject lastTradePrefab, content;

    public delegate void OnLatestTradeReceived();
    public static OnLatestTradeReceived onLatestTradeReceived;

    void Start()
    {
        GetHoldings();
        GetLatestTrades();
    }

    void GetHoldings()
    {
        StartCoroutine(GetHoldingsRequest());
    }

    void GetLatestTrades()
    {
        StartCoroutine(GetLatestTradesRequest());
    }

    IEnumerator GetHoldingsRequest()
    {
        Debug.Log("GetHoldingsRequest started.");
        UnityWebRequest www = UnityWebRequest.Get(holdingsURL);
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(1f);
            StartCoroutine(GetHoldingsRequest());
        }
        else
        {
            Debug.Log("Holdings : " + www.downloadHandler.text);
            holdingsString = www.downloadHandler.text;
            holdingsString = "{\"holdings\":" + holdingsString + "}";
            holdings = JsonUtility.FromJson<Holdings>(holdingsString);
            StockHolder.onHoldingsReceived(holdings);
            Invoke("GetHoldings", delayBetweenHoldings);
        }
    }

    IEnumerator GetLatestTradesRequest()
    {
        Debug.Log("GetHoldingsRequest started.");
        UnityWebRequest www = UnityWebRequest.Get(latestTradesURL);
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(1f);
            StartCoroutine(GetLatestTradesRequest());
        }
        else
        {
            Debug.Log("LatestTrades : " + www.downloadHandler.text);
            latestTradesString = www.downloadHandler.text;
            latestTradesString = "{\"latestTrade\":" + latestTradesString + "}";
            latestTrades = JsonUtility.FromJson<LatestTrades>(latestTradesString);
            GetLatestTrade(latestTrades.latestTrade);
            Invoke("GetLatestTrades", delayBetweenLatestTrades);
        }
    }

    void GetLatestTrade(LatestTrade latestTrade)
    {
        LastTradeHolder lastTradeHolder = Instantiate(lastTradePrefab, content.transform).GetComponent<LastTradeHolder>();
        RemoveTheTopTrade();
        lastTradeHolder.InitTradeData(latestTrade);
        onLatestTradeReceived();
        Invoke("GetLastTrade", 2f);
    }

    void RemoveTheTopTrade()
    {
        if(content.transform.childCount > 6)
            Destroy(content.transform.GetChild(0).gameObject);
    }
}
