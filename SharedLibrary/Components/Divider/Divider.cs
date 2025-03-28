using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace SharedLibrary.Components.Divider;

/// <summary>
/// 分隔器
/// </summary>
public class Divider : ComponentBase
{
    /// <summary>
    /// The content of the child.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The additional attributes.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new();

    /// <summary>
    /// The outer layer style.
    /// </summary>
    public string? OuterLayerStyle { get; set; }


    /// <summary>
    /// Method invoked when the component is ready to start, having received its
    /// initial parameters from its parent in the render tree.
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OuterLayerStyle ??= "display: block; height: 30px;";
    }


    /// <summary>
    /// Renders the component to the supplied <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder">RenderTreeBuilder</see>.
    /// </summary>
    /// <param name="builder">
    /// A <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder">RenderTreeBuilder</see> 
    /// that will receive the render output.
    /// </param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        int sequence = 0;

        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "style", OuterLayerStyle);
        builder.AddAttribute(sequence++, "@attributes", AdditionalAttributes);
        builder.OpenElement(sequence++, "span");
        builder.AddContent(sequence, ChildContent);
        builder.CloseComponent();
        builder.CloseComponent();

    }
}
