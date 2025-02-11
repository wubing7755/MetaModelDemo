class Helper
{
    public static string GetRandomString(int length)
    {
        return Guid.NewGuid().ToString("N").Substring(0, length);
    }

    // 生成指定范围内的随机整数
    public static int GetRandomInt(int min, int max)
    {
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        var scale = BitConverter.ToUInt32(bytes, 0);
        return (int)(min + (scale / (uint.MaxValue + 1.0) * (max - min + 1)));
    }

    // 检查字符串是否为空
    public static bool IsNullOrEmpty(string str)
    {
        return string.IsNullOrEmpty(str);
    }

    // 获取当前时间戳
    public static long GetTimeStamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    // 字符串转换为Base64
    public static string ToBase64(string input)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }

    // Base64转换为字符串
    public static string FromBase64(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}