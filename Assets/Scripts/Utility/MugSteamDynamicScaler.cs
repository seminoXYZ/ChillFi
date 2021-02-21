using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugSteamDynamicScaler : MonoBehaviour
{
    [Tooltip("How fast should the steam to be scaled to fit the mug rotation")]
    public float speed = 1f;
    [SerializeField]
    Vector3 minPoint, maxPoint;
    [SerializeField]
    bool forward;
    [SerializeField]
    float interpolationPoint = 1;
    [SerializeField]
    Transform anchor;
    [SerializeField]
    float offsetValue;
    
    private void Update()
    {
        if (forward)
            interpolationPoint += Time.deltaTime * speed * offsetValue;
        else
            interpolationPoint -= Time.deltaTime * speed * offsetValue;

        if (interpolationPoint >= 1)
        {
            forward = false;
            interpolationPoint = 1;
        }
            
        if(interpolationPoint <= 0)
        {
            forward = true;
            interpolationPoint = 0;
        }
        
        transform.localScale = Vector3.Lerp(minPoint, maxPoint, interpolationPoint);
    }
}
