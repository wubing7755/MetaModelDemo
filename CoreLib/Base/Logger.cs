namespace CoreLib.Base;

public static class Logger
{
    private static string[] _info = new string[1000];

    private static int index = 0;

    public static void Info(string msg)
    {
        _info[index] = msg;

        index++;
    }
}
