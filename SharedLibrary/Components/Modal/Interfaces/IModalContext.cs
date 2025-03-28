using SharedLibrary.Components.Modal.Models;

namespace SharedLibrary.Components.Modal.Interfaces;

/// <summary>
/// Modal上下文接口
/// </summary>
public interface IModalContext
{
    Type ModalType { get; }
    
    IModalOptions Options { get; }
    
    TaskCompletionSource<ModalResult> TaskSource { get; }
    
    IDictionary<string, object> Parameters { get; }
}