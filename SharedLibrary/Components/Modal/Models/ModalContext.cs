using SharedLibrary.Components.Modal.Interfaces;

namespace SharedLibrary.Components.Modal.Models;

public class ModalContext : IModalContext
{
    public Type ModalType { get; }
    
    public IModalOptions Options { get; }
    
    public TaskCompletionSource<ModalResult> TaskSource { get; }
    
    public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
    
    public ModalContext(Type modalType, IModalOptions options, TaskCompletionSource<ModalResult> tcs)
        => (ModalType, Options, TaskSource) = (modalType, options, tcs);
}