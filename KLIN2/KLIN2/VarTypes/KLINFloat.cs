namespace KLIN2.VarTypes;

internal class KLINFloat
{
    public string Name { get; set; }

    public const string Type = "float";

    public float Value { get; set; }

    public override string ToString()
    {
        return Name + " " + Type + " " + Value;
    }

    public void ParseAndAdd(string inString)
    {
        string[] str = inString.Split(' ');

        Name = str[0];
        Value = float.Parse(str[2]);
    }
}
