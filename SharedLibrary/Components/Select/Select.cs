

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary.Components;

public class Select : AWComponentBase
{
    [Parameter, NotNull, EditorRequired]
    public IEnumerable<string>? Options { get; set; } = Enumerable.Empty<string>();

    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public Func<string, string>? ValueChanged { get; set; }

    [Parameter]
    public string? Placeholder { get; set; } = "Please choose an option";

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        int seq = 0;

        builder.OpenElement(seq++, "div");

        builder.OpenElement(seq++, "select");
        builder.AddMultipleAttributes(seq++, AdditionalAttributes);
        builder.AddAttribute(seq++, "class", CssClass);
        builder.AddAttribute(seq++, "style", Style);
        builder.AddAttribute(seq++, "disabled", Disabled);

        builder.AddAttribute(seq++, "onchange", EventCallback.Factory.Create<ChangeEventArgs>(this, arg =>
        {
            var value = arg.Value?.ToString();
            if (value is not null)
            {
                Value = value;
                ValueChanged?.Invoke(value);
            }
        }));

        builder.OpenElement(seq++, "option");
        builder.AddAttribute(seq++, "value", "");
        builder.AddContent(seq++, Placeholder);
        builder.CloseElement();

        if (Options.Any())
        {
            foreach (var option in Options)
            {
                builder.OpenElement(seq++, "option");
                builder.AddAttribute(seq++, "value", option);
                builder.AddContent(seq++, option);
                builder.CloseElement();
            }
        }

        builder.CloseElement();


        builder.CloseElement();

    }
}
