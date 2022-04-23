namespace KLIN2.VarTypes;

internal class KLINString
{
    public string Name { get; set; }
   
    public const string Type = "string";
   
    public string Value { get; set; }

    public override string ToString()
    {
        return $"{Name} {Type} {Value},";
    }

    public void ParseAndAdd(string inString)
    {
        string[] str = inString.Split(' ');

        Name = str[0];
        Value = str[2];
    }
}
