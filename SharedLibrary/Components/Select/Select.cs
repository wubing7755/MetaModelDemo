

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
    public EventCallback<string>? ValueChanged { get; set; }

    [Parameter]
    public string? Placeholder { get; set; } = "Please choose an option";

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    [Parameter]
    public RenderFragment<string>? OptionTemplate { get; set; }

    private void OnValueChanged(ChangeEventArgs args)
    {
        var value = args.Value?.ToString();
        Value = value;
        if(ValueChanged.HasValue)
        {
            ValueChanged.Value.InvokeAsync(value);
        }
    }

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

        builder.AddAttribute(seq++, "value", Value);
        builder.AddAttribute(seq++, "onchange", OnValueChanged);

        builder.OpenElement(seq++, "option");
        builder.AddAttribute(seq++, "value", "");
        builder.AddAttribute(seq++, "selected", string.IsNullOrEmpty(Value));
        builder.AddContent(seq++, Placeholder);
        builder.CloseElement();

        if (Options.Any())
        {
            foreach (var option in Options)
            {
                builder.OpenElement(seq++, "option");
                builder.AddAttribute(seq++, "value", option);
                builder.AddAttribute(seq++, "selected", option == Value);

                if(OptionTemplate is not null)
                {
                    builder.AddContent(seq++, OptionTemplate(option));
                }
                else
                {
                    builder.AddContent(seq++, option);
                }
  
                builder.CloseElement();
            }
        }

        builder.CloseElement();


        builder.CloseElement();

    }
}
