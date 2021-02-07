using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatter
{
    public static string GetTwoDigitsValueWithMultiplier(ulong value, int digitsLimit)
    {
        string multiplied = "";

        if (value < 1000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value.ToString()));

        if (value > 999 && value < 1000000)
            multiplied = GetAmericanFormat(CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000f).ToString()), digitsLimit)) + "K";

        if (value > 999999 && value < 1000000000)
            multiplied = GetAmericanFormat(CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000000f).ToString()), digitsLimit)) + "M";

        if (value > 999999999)
            multiplied = GetAmericanFormat(CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000000000f).ToString()), digitsLimit)) + "B";
        
        return multiplied;
    }

    public static string GetTwoDigitsValueWithMultiplier(float value, int digitsLimit)
    {
        string multiplied = "";

        if (value < 1000)
            multiplied = GetAmericanFormat(FormatIntoTwoDecimalValue(value.ToString()));

        if (value > 999 && value < 1000000)
            multiplied = GetAmericanFormat(CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000f).ToString()), digitsLimit)) + "K";

        if (value > 999999 && value < 1000000000)
            multiplied = GetAmericanFormat(CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000000f).ToString()), digitsLimit)) + "M";

        if (value > 999999999)
            multiplied = GetAmericanFormat(CutLenghtTo(FormatIntoTwoDecimalValue((value / 1000000000f).ToString()), digitsLimit)) + "B";

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
            result = float.Parse(source.Substring(0, source.IndexOf(",")) + source.Substring(source.IndexOf(","), 3));
        }
        
        return result;
    }

    public static string GetAmericanFormat(string source)
    {
        string result = "";
        string integers = "";
        string decimals = "";

        if (source.Contains(","))
        {
            
            integers = source.Substring(0, source.IndexOf(","));

            if (integers.Length > 3 && integers.Length < 7)
                integers = integers.Insert(source.IndexOf(",") - 3, ",");
            else if (integers.Length > 6 && integers.Length < 10)
            {
                integers = integers.Insert(source.IndexOf(",") - 3, ",");
                integers = integers.Insert(source.IndexOf(",") - 6, ",");
            }


                


            decimals = source.Substring(source.IndexOf(",")).Replace(",", ".");

            result = integers + decimals;
            
        }
        else
        {
            integers = source;

            if (source.Length > 3 && source.Length < 7)
                integers = integers.Insert(integers.Length - 3, ",");

            if (source.Length > 6 && source.Length < 10)
                integers = integers.Insert(integers.Length - 6, ",");

            result = integers + ".00";
        }

        return result;
    }


}
