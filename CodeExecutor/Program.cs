namespace CodeExecutor;

public static class Program
{
    public static void Main(string[] args)
    {
#nullable disable warnings
        string nullableStr = null;

        Console.WriteLine(nullableStr);
    }
}
