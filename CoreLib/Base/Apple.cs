using System.Diagnostics;

namespace CoreLib.Base;

public class Apple
{
    private string Name { get; set; }
    private AppleType AppleType { get; set; }

    public Apple(string name, AppleType appleType)
    {
        Name = name;
        AppleType = appleType;
    }

    public (string, AppleType) GetApple()
    {
        return (Name, AppleType);
    }
    
    
    [Conditional("DEBUG")]
    public void AssertInvariants()
    {
        Debug.Assert(AppleType.GetType() == typeof(AppleType));
        
        Debug.Assert(Name != null);
    }
}

public enum AppleType
{
    Unrip,          // 未成熟
    PartiallyRipe,  // 部分成熟
    Ripe,           // 完全成熟
    Overripe        // 过熟
}