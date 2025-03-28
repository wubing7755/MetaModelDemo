using SharedLibrary.Components.Modal.Models;

namespace SharedLibrary.Components.Modal.Interfaces;

/// <summary>
/// 模态框
/// </summary>
public interface IModal
{
    Task<ModalResult> ShowAsync(IModalContext context);
    
    Task CloseAsync();
}