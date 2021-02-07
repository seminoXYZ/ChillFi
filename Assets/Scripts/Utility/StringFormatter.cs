using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatter
{
    public static string GetTwoDigitsValueWithMultiplier(ulong value, int digitsLimit)
    {
        string multiplied = "";

        if (value < 1000)
            multiplied = value.ToString();

        if (value > 999 && value < 1000000)
            multiplied = CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000f).ToString()), digitsLimit) + "K";

        if (value > 999999 && value < 1000000000)
            multiplied = CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000000f).ToString()), digitsLimit) + "M";

        if (value > 999999999)
            multiplied = CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000000000f).ToString()), digitsLimit) + "B";
        
        return multiplied;
    }

    public static string GetThreeDigitsValueWithMultiplier(float value, int digitsLimit)
    {
        string multiplied = "";

        if (value < 1000)
            multiplied = value.ToString();

        if (value > 999 && value < 1000000)
            multiplied = CutLenghtTo(FormatIntoThreeDecimalValue((value / 1000f).ToString()), digitsLimit) + "K";

        if (value > 999999 && value < 1000000000)
            multiplied = CutLenghtTo(FormatIntoThreeDecimalValue((value / 1000000f).ToString()), digitsLimit) + "M";

        if (value > 999999999)
            multiplied = CutLenghtTo(FormatIntoThreeDecimalValue((value / 1000000000f).ToString()), digitsLimit) + "B";

        return multiplied;
    }

    public static string FormatIntoTwoDecimalValue(string source)
    {
        string result = "";

        if (source.Contains(","))
        {
            if (source.Substring(source.IndexOf(",")).Length > 3)
                result = source.Substring(0, source.IndexOf(",")) + source.Substring(source.IndexOf(","), 3);
            else
                result = source.Substring(0, source.IndexOf(",")) + source.Substring(source.IndexOf(","));
        }
        else
            result = source;

        return result;
    }

    public static string FormatIntoThreeDecimalValue(string source)
    {
        string result = "";

        if (source.Contains(","))
        {
            if (source.Substring(source.IndexOf(",")).Length > 4)
                result = source.Substring(0, source.IndexOf(",")) + source.Substring(source.IndexOf(","), 4);
            else
                result = source.Substring(0, source.IndexOf(",")) + source.Substring(source.IndexOf(","));
        }
        else
            result = source;

        return result;
    }

    static string CutLenghtTo(string source, int lenght)
    {
        if (source.Length > 7)
            return source.Substring(0, lenght);
        else
            return source;
    }

    public static float GetCorrectFloat(string source)
    {
        source = source.Replace(".", ",");

        float result = 0;

        try
        {
            result = float.Parse(source);
        }
        catch (System.Exception e)
        {
            result = float.Parse(source.Substring(source.IndexOf(","), 4));
        }
        
        return result;
    }
}
