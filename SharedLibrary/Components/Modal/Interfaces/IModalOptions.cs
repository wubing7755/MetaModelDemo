using SharedLibrary.Components.Modal.Models;

namespace SharedLibrary.Components.Modal.Interfaces;

public interface IModalOptions
{
    ModalSize Size { get; }
    
    string Width { get; }
    
    string Height { get; }
    
    ModalPosition Position { get; }
    
    bool ShowCloseButton { get; }
    
    bool CloseOnBackdropClick { get; }
    
    bool CloseOnEsc { get; }
    
    bool DisableScrollLock { get; }
    
    ModalAnimation EnterAnimation { get; }
    
    ModalAnimation ExitAnimation { get; }
    
    int AnimationDuration { get; }
    
    string AriaLabeledBy { get; }
    
    string AriaDescription { get; }
}