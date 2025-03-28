using SharedLibrary.Components.Modal.Models;

namespace SharedLibrary.Components.Modal.Interfaces;

public interface IModalService
{
    Task<ModalResult> ShowAsync<T>(IModalOptions options);
    
    event Action<IModalContext> OnModalChanged;
    
    void CloseCurrent();
}