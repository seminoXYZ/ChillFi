using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShrimpyService : MonoBehaviour
{
    public static ShrimpyService instance;

    [SerializeField]
    string tickersURL, apiKey, tickers;

    void Awake()
    {
        instance = this;
    }

    public void GetTicker(string symbol)
    {
        StartCoroutine(GetTickerReq(symbol));
    }

    IEnumerator GetTickerReq(string symbol)
    {
        Debug.Log("Get " + symbol);
        UnityWebRequest www = UnityWebRequest.Get(tickersURL);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(60f);
            StartCoroutine(GetTickerReq(symbol));
        }
        else
        {
            tickers = www.downloadHandler.text;
            tickers = "{\"tickers\":" + tickers + "}";
            Tickers tickersData = JsonUtility.FromJson<Tickers>(tickers);
            StockHolder.onTickerReceived(tickersData);
        }
    }
}
