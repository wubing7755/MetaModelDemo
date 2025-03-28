using System.Xml;
using SharedLibrary.Components.Modal;
using SharedLibrary.Components.Modal.Models;
using Xunit.Abstractions;

namespace UtilsTest.WebModalTest.FuncTest;

public class ModalTest : TestBase
{
    public ModalTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

    [Fact]
    public void TestModal()
    {
        var apple = "Apple";
        var modal = new ModalResult(true, apple);

        Assert.IsType<string>(modal.GetData<string>());
        
        modal.Deconstruct(out var confirm, out var data);
        
        Assert.True(confirm);
        Assert.Equal(apple, data);
    }

    [Fact]
    public void TestModalOptions()
    {
        var modalOptions = new ModalOptions.Builder()
            .WithSize(ModalSize.Large)
            .WithPosition(ModalPosition.Top)
            .WithAnimations(ModalAnimation.FadeIn, ModalAnimation.FadeOut)
            .Build();
        
        Assert.Equal(ModalSize.Large, modalOptions.Size);
        Assert.Equal(ModalPosition.Top, modalOptions.Position);
        Assert.Equal(ModalAnimation.FadeIn, modalOptions.EnterAnimation);
        Assert.Equal(ModalAnimation.FadeOut, modalOptions.ExitAnimation);
    }
}