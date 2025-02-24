
namespace CoreLib.Base;

internal class TestValue
{
    public FileMode FileMode { get; set; }

    public void SetFileMode(FileMode fileModel)
    {
        FileMode = fileModel;
    }

    public void SetDynamicValue()
    {
        dynamic value = new DynamicClass();

        dynamic d = 1;
        var testSum = d + 3;
        // Rest the mouse pointer over testSum in the following statement.
        System.Console.WriteLine(testSum);

        dynamic obj = new Microsoft.CSharp.RuntimeBinder.RuntimeBinderException().Data;

        obj = "cc--glc";

        checked
        {
            int i = 123 + 456;
            Console.WriteLine(i);
        }

        unchecked
        {
            int i = 123 + 456;
            Console.WriteLine(i);
        }

        var tuple = (X: 1, Y: 2);
        var (x, y) = tuple;

        Console.WriteLine(x);
        Console.WriteLine(y);

        var tuple2 = (X: 0, Y: 1, Label: "The origin");
        var (x2, _, _) = tuple2;
    }
}


public enum FileMode
{
    CreateNew = 1,
    Create = 2,
    Open = 3,
    OpenOrCreate = 4,
    Truncate = 5,
    Append = 6,
}

public class DynamicClass
{
    public DynamicClass() { }

    public DynamicClass(int v) { }

    public void method1(int i) { }

    public void method2(int i) { }
}