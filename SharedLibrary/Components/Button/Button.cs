﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using SharedLibrary.Events;
using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary.Components;

public class Button : AWComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter, NotNull]
    public Action<MouseEventArgs>? OnClick { get; set; }

    [Parameter]
    public Action<MouseEventArgs>? OnMouseEnter { get; set; }

    [Parameter]
    public Action<MouseEventArgs>? OnMouseLeave { get; set; }

    protected override string BaseCssClass => "aw-btn";

    protected virtual string ButtonClass => BuildCssClass();

    protected virtual string ButtonStyle => BuildStyle();

    protected override void BuildComponent(RenderTreeBuilder builder)
    {
        int seq = 0;

        builder.OpenElement(seq++, "button");


        builder.AddMultipleAttributes(seq++, SafeAttributes);
        builder.AddAttribute(seq++, "class", ButtonClass);
        builder.AddAttribute(seq++, "style", ButtonStyle);
        builder.AddAttribute(seq++, "role", "button");

        if(Disabled)
        {
            builder.AddAttribute(seq++, "aria-disabled", "true");
            builder.AddAttribute(seq++, "disabled", "true");
        }

        builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, HandleClick));
        builder.AddAttribute(seq++, "onmouseenter", OnMouseEnter);
        builder.AddAttribute(seq++, "onmouseleave", OnMouseLeave);

        builder.AddContent(seq++, ChildContent);
        builder.CloseElement();
    }

    protected override bool IsAttributeAllowed(string attributeName)
    {
        // 允许 aria-* 和无障碍属性
        if (attributeName.StartsWith("aria-", StringComparison.OrdinalIgnoreCase) ||
            attributeName.Equals("role", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        // 允许父类允许的属性和以下按钮专用属性
        return base.IsAttributeAllowed(attributeName) ||
               attributeName.Equals("type", StringComparison.OrdinalIgnoreCase) ||
               attributeName.Equals("autofocus", StringComparison.OrdinalIgnoreCase);
    }

    private void HandleClick(MouseEventArgs args)
    {
        OnClick?.Invoke(args);

        EventBus.Publish<ButtonClickedEvent>(new ButtonClickedEvent(this.ObjectId.ToString()));
    }
}
