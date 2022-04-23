namespace KLIN2.VarTypes;

internal class KLINInt
{
    public string Name { get; set; }

    public const string Type = "int";

    public int Value { get; set; }

    public override string ToString()
    {
        return Name + " " + Type + " " + Value;
    }

    public void ParseAndAdd(string inString)
    {
        string[] str = inString.Split(' ');

        Name = str[0];
        Value = int.Parse(str[2]);
    }
}
