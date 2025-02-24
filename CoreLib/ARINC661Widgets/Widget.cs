using System.ComponentModel.DataAnnotations;

namespace CoreLib.ARINC661Widgets;

/// <summary>
/// 所有ARINC661 Widget的基类
/// </summary>
public abstract class Widget
{
    // 常量定义
    protected const string ERROR_INVALID_WIDGET_ID = "部件标识符不能为空或空白";
    protected const string ERROR_INVALID_WIDGET_TYPE = "部件类型不能为空或空白";
    protected const string ERROR_NEGATIVE_SIZE = "部件尺寸不能为负值";
    protected const string ERROR_INVALID_PARENT_MATCH = "子部件的ParentIdent必须匹配当前部件的WidgetIdent";

    // 基本标识属性
    public string WidgetIdent { get; }
    public string ParentIdent { get; }
    public string WidgetType { get; }

    // 状态属性
    public bool Visible { get; set; }
    public bool Enable { get; set; } = true;

    // 位置和尺寸
    public int PosX { get; protected set; }
    public int PosY { get; protected set; }
    public int SizeX { get; }
    public int SizeY { get; }

    // 子部件集合
    private readonly List<Widget> _children = new List<Widget>();
    public IReadOnlyList<Widget> Children => _children.AsReadOnly();

    public class WidgetConfig
    {
        public string WidgetIdent { get; set; }
        public string ParentIdent { get; set; }
        public string WidgetType { get; set; }
        public bool Visible { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }

    protected Widget(WidgetConfig config)
    {
        ValidateConstructorParameters(config.WidgetIdent, config.WidgetType, config.SizeX, config.SizeY);

        WidgetIdent = config.WidgetIdent;
        ParentIdent = config.ParentIdent;
        WidgetType = config.WidgetType;
        Visible = config.Visible;
        PosX = config.PosX;
        PosY = config.PosY;
        SizeX = config.SizeX;
        SizeY = config.SizeY;
    }

    private static void ValidateConstructorParameters(string widgetIdent, string widgetType, int sizeX, int sizeY)
    {
        if (string.IsNullOrWhiteSpace(widgetIdent))
            throw new ArgumentException(ERROR_INVALID_WIDGET_ID, nameof(widgetIdent));

        if (string.IsNullOrWhiteSpace(widgetType))
            throw new ArgumentException(ERROR_INVALID_WIDGET_TYPE, nameof(widgetType));

        if (sizeX < 0 || sizeY < 0)
            throw new ArgumentException(ERROR_NEGATIVE_SIZE);
    }

    public virtual void UpdateProperties(Dictionary<string, object> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        if (properties.TryGetValue("Visible", out object visibleValue))
            Visible = (bool)visibleValue;

        if (properties.TryGetValue("Enable", out object enableValue))
            Enable = (bool)enableValue;
    }

    // 子部件管理方法
    public void AddChild(Widget child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (child.ParentIdent != WidgetIdent)
            throw new ArgumentException(ERROR_INVALID_PARENT_MATCH);

        _children.Add(child);
    }

    public bool RemoveChild(Widget child) => _children.Remove(child);

    public Widget? FindChild(string widgetIdent) => 
        _children.FirstOrDefault(w => w.WidgetIdent == widgetIdent);

    public virtual (int X, int Y) GetAbsolutePosition() => (PosX, PosY);
}

