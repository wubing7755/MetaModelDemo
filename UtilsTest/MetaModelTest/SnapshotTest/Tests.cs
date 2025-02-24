using System.Runtime.CompilerServices;
using CoreLib.Base;
using CoreLib.Utils;

namespace UtilsTest.MetaModelTest.SnapshotTest;

public class Tests
{
    [Fact]
    public Task VerifyAppleData()
    {
        // arrange
        var apple = new Apple("Good Apple", AppleType.PartiallyRipe);
        
        // act
        var data = apple.GetApple();
        
        // verify
        return Verify(data);
    }

    //private readonly Apple _sut = new("Good Apple", AppleType.ParitallyRipe);
    
    // [Fact]
    // public void Assert_apple_is_granny_smith()
    // {
    //     var apple = _sut.GetApple();
    //     Assert.Equal("Granny Smith", apple.Name);
    // }
}

// public class StaticSettings
// {
//     [Fact]
//     public Task Test() =>
//         Verify("String to verify");
// }
//
// public static class StaticSettingsUsage
// {
//     [ModuleInitializer]
//     public static void Initialize()
//     {
//         VerifierSettings.AddScrubber(_ => _.Replace("String to verify", "new value"));
//     }
// }


