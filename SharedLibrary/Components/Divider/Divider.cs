using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace SharedLibrary.Components;

/// <summary>
/// 分隔器
/// </summary>
public sealed class Divider : ComponentBase
{
    [CascadingParameter]
    public string? CascadingStyle { get; set; }

    [Parameter]
    public string? Style { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Style ??= CascadingStyle ??= "display: block; height: 30px;";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        int sequence = 0;

        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "style", Style);
        builder.AddMultipleAttributes(sequence++, AdditionalAttributes);
        builder.OpenElement(sequence++, "span");
        builder.AddContent(sequence, ChildContent);
        builder.CloseElement();
        builder.CloseElement();
    }
}
