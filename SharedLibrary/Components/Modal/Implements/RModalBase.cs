using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace SharedLibrary.Components.Modal.Implements;

public class RModalBase : ComponentBase, IDisposable
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        int sequence = 0;
        
        // add a simple modal component
        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", "modal");
        builder.AddContent(sequence, "<div class=\"modal-dialog\">");
        builder.AddContent(sequence, "</div>");
        builder.CloseComponent();
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}