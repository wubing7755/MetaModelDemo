using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Components.Modal.Interfaces;

/// <summary>
/// 动画策略
/// </summary>
public interface IAnimationStrategy : IDisposable
{
    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="element">元素引用</param>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    Task ApplyShowAsync(ElementReference element, int duration);
    
    /// <summary>
    /// 隐藏
    /// </summary>
    /// <param name="element">元素引用</param>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    Task ApplyHideAsync(ElementReference element, int duration);
}
