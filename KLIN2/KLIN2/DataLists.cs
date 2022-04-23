using KLIN2.VarTypes;

namespace KLIN2;

internal class DataLists
{
    public List<KLINString> Strings = new();
    public List<KLINInt> Ints = new();
    public List<KLINFloat> Floats = new();

    public override string ToString()
    {
        string output = "";

        foreach (KLINString str in Strings)
        {
            output += $"{str}\n";
        }

        foreach (KLINInt str in Ints)
        {
            output += $"{str}\n";
        }

        foreach (KLINFloat str in Floats)
        {
            output += $"{str}\n";
        }

        return output;
    }
}
