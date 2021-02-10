using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatter
{
    public static string GetTwoDigitsValueWithMultiplier(ulong value, int digitsLimit)
    {
        string multiplied = "";

        if (value < 1000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value));

        if (value > 999 && value < 1000000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value / 1000f)) + "K";

        if (value > 999999 && value < 1000000000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value / 1000000f)) + "M";

        if (value > 999999999)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value / 1000000000f)) + "B";
        
        return multiplied;
    }

    public static string GetTwoDigitsValueWithMultiplier(float value, int digitsLimit)
    {
        string multiplied = "";

        if (value < 1000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value));

        if (value > 999 && value < 1000000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value / 1000f)) + "K";

        if (value > 999999 && value < 1000000000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value / 1000000f)) + "M";

        if (value > 999999999)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value / 1000000000f)) + "B";

        return multiplied;
    }

    public static float FormatIntoTwoDecimalValue(float source)
    {
        return (float)System.Math.Round(source * 100f) / 100f;
    }

    public static float FormatIntoTwoDecimalValue(ulong source)
    {
        return (float)System.Math.Round(source * 100f) / 100f;
    }
    
    public static string GetAmericanFormat(float source)
    {
        string result = source.ToString();

        if (source > 999.99f && source < 1000000)
        {
            if(result.Contains("."))
                result = result.Insert(result.IndexOf(".") - 3, ",");
            else
                result = result.Insert(result.Length - 3, ",");
        }
        

        if (source > 999999.99f && source < 1000000000)
        {
            if(result.Contains("."))
                result = result.Insert(result.IndexOf(".") - 6, ",");
            else
                result = result.Insert(result.Length - 6, ",");
        }
            

        return result;
    }
}
