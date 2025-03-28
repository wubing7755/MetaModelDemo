using Microsoft.JSInterop;
using SharedLibrary.Components.Modal.Interfaces;
using SharedLibrary.Components.Modal.Models;

namespace SharedLibrary.Components.Modal.Services;

public class ModalService : IModalService, IDisposable
{
    private readonly Stack<IModalContext> _modalStack = new();

    private readonly IJSRuntime _jsRuntime;

    public event Action<IModalContext>? OnModalChanged;

    public ModalService(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;
    
    private IJSObjectReference? _jsModule;

    public async Task<ModalResult> ShowAsync<T>(IModalOptions options)
    {
        var tcs = new TaskCompletionSource<ModalResult>();

        var context = new ModalContext(typeof(T), options, tcs);

        _jsModule ??= await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/SharedLibrary/js/modal-interop.js");

        if (!options.DisableScrollLock)
            await _jsModule!.InvokeVoidAsync("lockScroll");

        _modalStack.Push(context);
        OnModalChanged?.Invoke(context);
        return await tcs.Task;
    }

    public async void CloseCurrent()
    {
        if(_modalStack.TryPop(out var context))
        {
            context.TaskSource.TrySetResult(ModalResult.Cancel());

            OnModalChanged?.Invoke(context);

            _jsModule ??= await _jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", 
                "./_content/SharedLibrary/js/modal-interop.js");
            
            if(!context.Options.DisableScrollLock && _modalStack.Count == 0)
                await _jsModule!.InvokeVoidAsync("unlockScroll");
        }
    }

    public void Dispose()
    {
        while(_modalStack.TryPop(out var context))
            context.TaskSource.TrySetCanceled();
    }
}