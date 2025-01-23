using System.Runtime.CompilerServices;
using CoreLib.Utils;

namespace UtilsTest.MetaModelTest.SnapshotTest;

public class Tests
{
    private static readonly VerifySettings Settings;
    
    static Tests()
    {
        Settings = new VerifySettings();
        Settings.UseDirectory("snapshots");
    }
    
    private readonly AppleService _sut = new();
    
    [Fact]
    public void Assert_apple_is_granny_smith()
    {
        var apple = _sut.GetApple();
        Assert.Equal("Granny Smith", apple.Name);
    }
    
    [Fact]
    public Task Verify_apple_is_granny_smith()
    {
        // arrange
        var service = new AppleService();
        // act 
        var apple = service.GetApple(); 
        
        
        // verify    
        return Verify(apple, Settings);
    }
}

public class StaticSettings
{
    [Fact]
    public Task Test() =>
        Verify("String to verify");
}

public static class StaticSettingsUsage
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.AddScrubber(_ => _.Replace("String to verify", "new value"));
    }
}


