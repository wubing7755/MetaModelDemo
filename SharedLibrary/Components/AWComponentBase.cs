using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Components;

public class AWComponentBase : ComponentBase, IDisposable
{
    [Parameter]
    public string? CssClass { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    public virtual void Dispose() {}
}
