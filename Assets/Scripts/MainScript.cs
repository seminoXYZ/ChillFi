using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Text chatText;
    public YoutubeManager youtubeManager;
    void Start()
    {
        UIManager.Instance.Initialize(chatText);
        youtubeManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.Refresh();
        youtubeManager.Refresh();
    }
}
