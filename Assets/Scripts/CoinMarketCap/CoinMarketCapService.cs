using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CoinMarketCapService : MonoBehaviour
{
    [SerializeField]
    string apiKey;

    [SerializeField]
    string listingsURL;

    [SerializeField]
    string listingsString;

    [SerializeField]
    ListingsLatest listings;

    [SerializeField]
    float delayBetweenRequests = 30f;

    private void Start()
    {
        GetPositions();
    }

    void GetPositions()
    {
        StartCoroutine(GetListingReq());
    }

    IEnumerator GetListingReq()
    {
        UnityWebRequest www = UnityWebRequest.Get(listingsURL);
        www.SetRequestHeader("X-CMC_PRO_API_KEY", apiKey);
        www.SetRequestHeader("Accepts", "application/json");
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            yield return new WaitForSeconds(60f);
            StartCoroutine(GetListingReq());
        }
        else
        {
            listingsString = www.downloadHandler.text;
            listingsString = "{\"listings\":" + listingsString + "}";
            listings = JsonUtility.FromJson<ListingsLatest>(listingsString);
            StockHolder.onListingsReceived(listings);
            Invoke("GetPositions", delayBetweenRequests);
        }
    }
}
