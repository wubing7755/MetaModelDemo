using System.Text.RegularExpressions;
using SharedLibrary.Components.Modal.Interfaces;

namespace SharedLibrary.Components.Modal.Models;

/// <summary>
/// 模态框配置选项
/// </summary>
public sealed class ModalOptions : IModalOptions
{
    /// <summary>
    /// 预设尺寸模板
    /// </summary>
    public ModalSize Size { get; set; } = ModalSize.Medium;
    
    /// <summary>
    /// 自定义高度
    /// </summary>
    /// <remarks>优先级高于Size</remarks>
    public string Width { get; set; }
    
    /// <summary>
    /// 自定义宽度
    /// </summary>
    /// <remarks>优先级高于Size</remarks>
    public string Height { get; set; }
    
    /// <summary>
    /// 模态框定位模式
    /// </summary>
    public ModalPosition Position { get; set; } = ModalPosition.Center;
    
    public bool ShowCloseButton { get; set; } = true;
    
    /// <summary>
    /// 点击遮罩层是否关闭
    /// </summary>
    public bool CloseOnBackdropClick { get; set; } = true;
    
    public bool CloseOnEsc { get; set; } = true;
    
    /// <summary>
    /// 禁用滚动条锁定
    /// </summary>
    public bool DisableScrollLock { get; set; }

    #region 动画配置

    public ModalAnimation EnterAnimation { get; set; } = ModalAnimation.FadeIn;
    
    public ModalAnimation ExitAnimation { get; set; } = ModalAnimation.FadeOut;

    /// <summary>
    /// 动画持续时间（ms）
    /// </summary>
    public int AnimationDuration { get; set; } = 300;

    #endregion

    #region 无障碍访问

    /// <summary>
    /// ARIA 标签ID
    /// </summary>
    public string AriaLabeledBy { get; set; }

    /// <summary>
    /// ARIA 模态描述
    /// </summary>
    public string AriaDescription { get; set; }
    
    #endregion

    #region 构建模式

    private ModalOptions() { }

    public class Builder
    {
        private readonly ModalOptions _options = new();

        public Builder WithSize(ModalSize size)
        {
            _options.Size = size;
            return this;
        }

        public Builder WithCustomSize(string width, string height)
        {
            _options.Width = width;
            _options.Height = height;
            return this;
        }

        public Builder WithPosition(ModalPosition position)
        {
            _options.Position = position;
            return this;
        }
        
        public Builder DisableCloseActions(bool closeOnBackdrop = false, bool closeOnEsc = false)
        {
            _options.CloseOnBackdropClick = closeOnBackdrop;
            _options.CloseOnEsc = closeOnEsc;
            return this;
        }
        
        public Builder WithAnimations(ModalAnimation enter, ModalAnimation exit, int duration = 300)
        {
            _options.EnterAnimation = enter;
            _options.ExitAnimation = exit;
            _options.AnimationDuration = duration;
            return this;
        }
        
        public Builder WithAccessibility(string labelledBy, string description)
        {
            _options.AriaLabeledBy = labelledBy;
            _options.AriaDescription = description;
            return this;
        }

        public ModalOptions Build()
        {
            // 尺寸校验逻辑
            if (!string.IsNullOrEmpty(_options.Width) && !IsValidSize(_options.Width))
                throw new ArgumentException("Invalid width format");
            
            if (!string.IsNullOrEmpty(_options.Height) && !IsValidSize(_options.Height))
                throw new ArgumentException("Invalid height format");
            
            return _options;
        }
        
        private static bool IsValidSize(string value) 
            => Regex.IsMatch(value, @"^(\d+(\.\d+)?(px|%|rem|em)|auto)$");
    }

    #endregion
}

public enum ModalSize
{
    Small,
    Medium,
    Large,
    FullScreen
}

public enum ModalPosition
{
    Center,
    Top,
    Bottom,
    Left,
    Right,
}

public enum ModalAnimation
{
    None,
    FadeIn,
    FadeOut,
    ZoomIn
}