using System;

public static class NumberShortener
{
    public static string ShortenNumber(float number)
    {
        string suffix;
        int exp;

        if (number >= GetPower(10, 3) && number < GetPower(10, 6))
        {
            suffix = "K";
            exp = 3;
        }
        else if (number >= GetPower(10, 6) && number < GetPower(10, 9))
        {
            suffix = "M";
            exp = 6;
        }
        else if (number >= GetPower(10, 9) && number < GetPower(10, 12))
        {
            suffix = "B";
            exp = 9;
        }
        else if (number >= GetPower(10, 12) && number < GetPower(10, 15))
        {
            suffix = "T";
            exp = 12;
        }
        else
        {
            suffix = "";
            exp = 0;
        }

        var divisor = GetPower(10, exp);

        if (exp == 0)
        {
            return Convert.ToString(Math.Truncate(number / divisor));
        }
        
        return Math.Round(number / divisor, 2) + " " + suffix;
    }

    private static float GetPower(int a, int b)
    {
        return (float) Math.Pow(a, b);
    }
}