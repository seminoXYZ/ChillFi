using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class YoutubeManager : MonoBehaviour
{
    // private static YoutubeManager _instance;
    //
    // public static YoutubeManager Instance =>
    //     _instance ?? (_instance = new YoutubeManager());

    private string _chatTest = "";

    // private YoutubeManager()
    // {
    // }

    public void Initialize()
    {
        StartCoroutine(GetLiveChat());
    }

    public void Refresh()
    {
    }

    IEnumerator GetLiveChat()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get(Constants.ChatEndPoint);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                _chatTest = www.error;
            }
            else
            {
                _chatTest = www.downloadHandler.text;
                UIManager.Instance.UpdateChatText(_chatTest);
            }

            yield return new WaitForSeconds(5f);
        }
    }
}