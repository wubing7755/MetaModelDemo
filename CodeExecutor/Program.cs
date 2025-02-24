using CoreLib.Base;

namespace CodeExecutor;

public static class Program
{
    public static void Main(string[] args)
    {
        // #nullable disable warnings
        //         string nullableStr = null;
        //
        //         Console.WriteLine(nullableStr);

        //Apple apple = new Apple("苹果", AppleType.Ripe);

        //apple.AssertInvariants();

        Logger logger = new Logger();

        logger.LogInfo("info log");
    }
}
