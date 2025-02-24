using System.Collections;

namespace CoreLib.Base;

public class Logger : LoggerBase
{
    private static string[] _info = new string[1000];

    private static int index = 0;

    public static void Info(string msg)
    {
        _info[index] = msg;

        index++;
    }

    public void LogInfo(string info)
    {
        _logger.Information("{0}: ", info);
    }
}
