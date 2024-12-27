using CoreLib.Utils;

namespace Launch.CodeExecutor;

internal class Program
{
    internal static void Main(string[] args)
    {
        ObjectJsonConverter.SaveToJsonTest();

        ObjectJsonConverter.CsvTest();

        ObjectJsonConverter.LoadFromJsonTest();

    }
}