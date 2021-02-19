using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyLightController : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        ChillFiAPIHandler.onLatestTradeReceived += OnLatestTrade;
    }

    void OnLatestTrade()
    {
        anim.SetTrigger("On");
    }
}
