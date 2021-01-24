using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMotionHandler : MonoBehaviour
{
    [SerializeField]
    float cloudSpeed = 0.2f;
    [SerializeField]
    float leftBorder, respawnDistance;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * cloudSpeed);
        if (transform.position.x > leftBorder)
            transform.Translate(-Vector3.right * respawnDistance);
    }
}
