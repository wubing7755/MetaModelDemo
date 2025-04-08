using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary.Components;

public class Button : AWComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter, NotNull]
    public Action<MouseEventArgs>? OnClick { get; set; }

    [Parameter]
    public Action<MouseEventArgs>? OnMouseLeave { get; set; }

    [Parameter]
    public Action<MouseEventArgs>? OnMouseEnter { get; set; }

    private EventCallback<MouseEventArgs> _onClick;

    private EventCallback<MouseEventArgs> _onMouseLeave;

    private EventCallback<MouseEventArgs> _onMouseEnter;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        _onClick = EventCallback.Factory.Create(this, OnClick);
        _onMouseLeave = EventCallback.Factory.Create(this, OnMouseLeave ?? (_ => { }));
        _onMouseEnter = EventCallback.Factory.Create(this, OnMouseEnter ?? (_ => { }));

        int seq = 0;

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", CssClass);

        if (!string.IsNullOrEmpty(Style))
        {
            builder.AddAttribute(seq++, "style", Style);
        }

        builder.OpenElement(seq++, "button");
        builder.AddAttribute(seq++, "onclick", _onClick);
        builder.AddAttribute(seq++, "onmouseenter", _onMouseEnter);
        builder.AddAttribute(seq++, "onmouseleave", _onMouseLeave);
        builder.AddAttribute(seq++, "disabled", Disabled);
        builder.AddContent(seq++, ChildContent);
        builder.CloseElement();

        builder.CloseElement();

        _ = InvokeAsync(() => { Console.WriteLine("Button Component is triggered."); });
    }
}
