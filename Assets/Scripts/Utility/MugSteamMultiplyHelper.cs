using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSteamMultiplyHelper : MonoBehaviour
{
    [SerializeField]
    GameObject steamPrefab;
    [SerializeField]
    float steamsOffsetAngle = 1;
    
    [ContextMenu("InstantiateSteams")]
    void InstantiateSteams()
    {
        int steamsQuantity = (int)(360f / steamsOffsetAngle);
        for(int s = 0; s < steamsQuantity; s++)
        {
            Instantiate(steamPrefab, transform).GetComponent<Transform>().Rotate(new Vector3(0, steamsOffsetAngle * s, 0));
        }
    }
}
