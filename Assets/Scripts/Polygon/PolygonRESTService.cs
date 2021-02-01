using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PolygonRESTService : MonoBehaviour
{
    public static PolygonRESTService instance;

    [SerializeField]
    string URL;
    [SerializeField]
    string APIKey;

    private void Awake()
    {
        instance = this;
    }

    public void GetTicker(string stock)
    {
        
        StartCoroutine(GetTickerRequest(stock));
    }

    IEnumerator GetTickerRequest(string symbol)
    {
        Debug.Log("Get " + symbol);
        UnityWebRequest www = UnityWebRequest.Get(URL.Insert(39, symbol) + APIKey);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(10f + Random.Range(0f, 3f));
            StartCoroutine(GetTickerRequest(symbol));
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Ticker ticker = JsonUtility.FromJson<Ticker>(www.downloadHandler.text);
            StockHolder.onTickerReceived(ticker);
        }
    }
}
