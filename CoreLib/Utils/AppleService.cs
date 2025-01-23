namespace CoreLib.Utils;

public class AppleService
{
    public Apple GetApple() => new Apple("Granny Smith", "Green");
}

public class Apple
{
    public string Name { get; set; }
    public string Color { get; set; }
    private AppleType AppleType { get; set; } = AppleType.Unrip;

    public Apple(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public bool SetAppleType(AppleType appleType)
    {
        AppleType = appleType;
        
        return true;
    }
}

public enum AppleType
{
    Unrip,          // 未成熟
    ParitallyRipe,  // 部分成熟
    Ripe,           // 完全成熟
    Overripe        // 过熟
}