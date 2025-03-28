using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedLibrary.Components.Modal.Interfaces;

namespace SharedLibrary.Components.Modal.Models;

public class FadeAnimationStrategy : IAnimationStrategy
{
    private readonly IJSRuntime _js;

    private IJSObjectReference? _module;

    public FadeAnimationStrategy(IJSRuntime js) => _js = js;

    public async Task ApplyShowAsync(ElementReference element, int duration)
    {
        _module ??= await _js.InvokeAsync<IJSObjectReference>(
            "import", 
            "./_content/SharedLibrary/js/animation.js");
       
        await _module.InvokeVoidAsync("fadeIn", element, duration);
    }

    public async Task ApplyHideAsync(ElementReference element, int duration)
    {
        if (_module != null)
            await _module.InvokeVoidAsync("fadeOut", element, duration);
    }

    public void Dispose() => _module?.DisposeAsync();
}
