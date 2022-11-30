using System.Text;

namespace KLIN2.Extensions;

public static class KlinInstanceHelpers
{
    public static string ParseToString(this Dictionary<string, bool> pairs)
    {
        StringBuilder outputStr = new();

        foreach (var pair in pairs)
        {
            outputStr.Append($"{pair.Key} {pair.Value}\n");
        }
        return outputStr.ToString();
    }

    public static string ParseToString(this Dictionary<string, int> pairs)
    {
        StringBuilder outputStr = new();

        foreach (var pair in pairs)
        {
            outputStr.Append($"{pair.Key} {pair.Value}\n");
        }
        return outputStr.ToString();
    }

    public static string ParseToString(this Dictionary<string, float> pairs)
    {
        StringBuilder outputStr = new();

        foreach (var pair in pairs)
        {
            outputStr.Append($"{pair.Key} {pair.Value}\n");
        }
        return outputStr.ToString();
    }

    public static string ParseToString(this Dictionary<string, string> pairs)
    {
        StringBuilder outputStr = new();

        foreach (var pair in pairs)
        {
            outputStr.Append($"{pair.Key} {pair.Value}\n");
        }
        return outputStr.ToString();
    }
}