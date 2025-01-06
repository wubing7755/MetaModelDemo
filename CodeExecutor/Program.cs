using CoreLib.Base;
using CoreLib.Utils;

namespace Launch.CodeExecutor;

internal class Program
{
    internal static void Main(string[] args)
    {
        //ObjectJsonConverter.SaveToJsonTest();

        //ObjectJsonConverter.CsvTest();

        //ObjectJsonConverter.LoadFromJsonTest();


        CArray<int> arr = new CArray<int>(5);

        for (int i = 0; i < 5; i++)
        {
            arr.InsertValue(i, i);
        }

        Console.WriteLine("{0}", arr.FindValue(3));
    }
}