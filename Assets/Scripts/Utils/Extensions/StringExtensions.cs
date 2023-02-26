using System;
using System.Text;

namespace RTLOL.Utilities
{
public static class StringExtensions
{
    public static string RemoveWhitespace(this string str)
    {
        return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
    }

    public static string RemovePunctuation(this string str)
    {
        var sb = new StringBuilder();
        foreach (char c in str)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }
        return sb.ToString();
    }

    public static string RemoveWhitespaceAndPunctuation(this string str)
    {
        str = RemoveWhitespace(str);
        str = RemovePunctuation(str);
        return str;
    }

    public static string GetStringBetweenCharacters(this string input, char charFrom, char charTo)
    {
        int posFrom = input.IndexOf(charFrom);
        if (posFrom != -1) //if found char
        {
            int posTo = input.IndexOf(charTo, posFrom + 1);
            if (posTo != -1) //if found char
            {
                return input.Substring(posFrom + 1, posTo - posFrom - 1);
            }
        }

        return string.Empty;
    }

}


}