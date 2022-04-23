using KLIN2;

namespace Debug;

internal static class DebugClass
{
    private static void Main()
    {
        KlinInstance klinInstance = new();
        if (klinInstance.GetValue("Yes", false))
        {
            Console.WriteLine("Yeah!");

            klinInstance.SetValue("Yes", false);
            return;
        }
        klinInstance.SetValue("Yes", true);
    }
}
