using KLIN2;

namespace Debug;

internal static class DebugClass
{
    private static void Main()
    {
        KlinInstance klinInstance = new();
       // if (klinInstance.GetBoolValue("Yes"))
        klinInstance.SetValue("Hell", "GotoHell");

        Console.WriteLine(klinInstance.GetStringValue("Hell", "..."));
    }
}
