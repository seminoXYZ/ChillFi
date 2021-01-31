using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCalculator : MonoBehaviour
{
    [SerializeField]
    Color color1, color2, middleColor;

    [ContextMenu("Calculate Middle Color")]
    public void CalculateMiddleColor()
    {
        middleColor = Color.Lerp(color1, color2, 0.5f);
    }
}
