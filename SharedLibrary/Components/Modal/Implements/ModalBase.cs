using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedLibrary.Components.Modal.Interfaces;
using SharedLibrary.Components.Modal.Models;

namespace SharedLibrary.Components.Modal.Implements;

/// <summary>
/// 模态框基类
/// </summary>
public abstract class ModalBase : ComponentBase, IDisposable
{
    [CascadingParameter] 
    protected ModalContainer? Container { get; set; }

    [Inject] 
    protected IJSRuntime JsRuntime { get; set; } = null!;
    
    protected ElementReference ModalRef;
    
    protected IModalContext Context = null!;

    private IAnimationStrategy? _animation;
    
    private DotNetObjectReference<ModalBase>? _dotNetRef;
    
    private bool _isDisposed;
    
    protected override async Task OnInitializedAsync()
    {
        _dotNetRef = DotNetObjectReference.Create(this);
        await SetupAccessibility();
    }
    
    public async Task<ModalResult> ShowAsync(IModalContext context)
    {
        Context = context;
        _animation = CreateAnimationStrategy();
        await ApplyShowAnimation();
        return await Context.TaskSource.Task;
    }
    
    public async Task CloseAsync()
    {
        await ApplyHideAnimation();
        Context.TaskSource.TrySetResult(ModalResult.Cancel());
        Container?.RemoveModal(Context);
        Dispose();
    }
    
    private async Task ApplyShowAnimation()
    {
        await InvokeAsync(StateHasChanged);
        await _animation!.ApplyShowAsync(ModalRef, Context.Options.AnimationDuration);
        await FocusFirstElement();
    }
    
    private async Task ApplyHideAnimation()
    {
        await _animation!.ApplyHideAsync(ModalRef, Context.Options.AnimationDuration);
        await InvokeAsync(StateHasChanged);
    }
    
    private IAnimationStrategy CreateAnimationStrategy()
    {
        return Context.Options.ExitAnimation switch
        {
            ModalAnimation.FadeIn => new FadeAnimationStrategy(JsRuntime),
            ModalAnimation.ZoomIn => new ZoomAnimationStrategy(JsRuntime),
            _ => new DefaultAnimationStrategy(JsRuntime)
        };
    }
    
    private async Task SetupAccessibility()
    {
        await JsRuntime.InvokeVoidAsync("setupModal",
            ModalRef,
            _dotNetRef,
            Context.Options.AriaLabeledBy,
            Context.Options.AriaDescription);
    }
    
    private async Task FocusFirstElement()
    {
        await JsRuntime.InvokeVoidAsync("focusFirstInteractive", ModalRef);
    }
    
    public void Dispose()
    {
        if (_isDisposed) return;

        _dotNetRef?.Dispose();
        _animation?.Dispose();
        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}