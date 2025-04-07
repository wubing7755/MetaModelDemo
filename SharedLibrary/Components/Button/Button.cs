

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary.Components;

public class Button : ComponentBase, IDisposable
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter, NotNull]
    public Func<MouseEventArgs, Task>? OnClick { get; set; }


    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        int seq = 0;

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "style", "");

        builder.OpenElement(seq++, "button");
        builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
        builder.AddContent(seq++, ChildContent);
        builder.CloseElement();

        builder.CloseElement();

    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
