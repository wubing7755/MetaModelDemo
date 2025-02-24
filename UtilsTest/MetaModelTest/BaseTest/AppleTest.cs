using CoreLib.Base;
using Xunit.Abstractions;

namespace UtilsTest.MetaModelTest.BaseTest;

public class AppleTest : TestBase
{
    public AppleTest(ITestOutputHelper testOutput) : base(testOutput) {}
    
    [Fact]
    public void TestApple()
    {
        Apple apple = new Apple("苹果", AppleType.Ripe);
        
        _logger.Information("------------{0}------------", 
            apple.GetApple().Item1);
    }
}