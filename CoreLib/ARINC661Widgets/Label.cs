using CoreLib.ARINC661Widgets;

public class Label : Widget
{
    // 常量定义
    private const string WIDGET_TYPE = "A661_LABEL";
    private const string ERROR_TEXT_TOO_LONG = "标签文本长度不能超过最大长度 {0}";

    // 常用参数
    public bool Anonymous { get; }
    public string StyleSet { get; set; }
    
    // 特定参数
    private string _labelString;
    public string LabelString
    {
        get => _labelString;
        set
        {
            ValidateLabelString(value);
            _labelString = value;
        }
    }
    
    public int MaxStringLength { get; }
    public bool MotionAllowed { get; }
    public float RotationAngle { get; set; }
    public string Font { get; set; }
    public int ColorIndex { get; set; }
    public LabelAlignment Alignment { get; }

    public enum LabelAlignment
    {
        BottomCenter,
        BottomLeft,
        BottomRight,
        Center,
        Left,
        Right,
        TopCenter,
        TopLeft,
        TopRight
    }

    public class LabelConfig
    {
        public bool Anonymous { get; set; }
        public string StyleSet { get; set; }
        public string LabelString { get; set; }
        public int MaxStringLength { get; set; }
        public bool MotionAllowed { get; set; }
        public float RotationAngle { get; set; }
        public string Font { get; set; }
        public int ColorIndex { get; set; }
        public LabelAlignment Alignment { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }

    public Label(
        string widgetIdent,
        string parentIdent,
        bool visible,
        LabelConfig config)
        : base(new WidgetConfig()
        {
            WidgetIdent = widgetIdent,
            ParentIdent = parentIdent,
            Visible = visible,
            WidgetType = WIDGET_TYPE,
            PosX = config.PosX,
            PosY = config.PosY,
            SizeX = config.SizeX,
            SizeY = config.SizeY
        })
    {
        MaxStringLength = config.MaxStringLength;
        Anonymous = config.Anonymous;
        StyleSet = config.StyleSet;
        MotionAllowed = config.MotionAllowed;
        RotationAngle = config.RotationAngle;
        Font = config.Font;
        ColorIndex = config.ColorIndex;
        Alignment = config.Alignment;

        LabelString = config.LabelString;
    }

    private void ValidateLabelString(string value)
    {
        if (value?.Length > MaxStringLength)
        {
            throw new ArgumentException(string.Format(ERROR_TEXT_TOO_LONG, MaxStringLength));
        }
    }

    public override void UpdateProperties(Dictionary<string, object> properties)
    {
        base.UpdateProperties(properties);

        if (Anonymous) return;

        if (properties.TryGetValue("LabelString", out object labelStringValue))
            LabelString = (string)labelStringValue;

        if (properties.TryGetValue("StyleSet", out object styleSetValue))
            StyleSet = (string)styleSetValue;

        if (MotionAllowed)
        {
            if (properties.TryGetValue("PosX", out object posXValue))
                PosX = (int)posXValue;

            if (properties.TryGetValue("PosY", out object posYValue))
                PosY = (int)posYValue;

            if (properties.TryGetValue("RotationAngle", out object rotationValue))
                RotationAngle = (float)rotationValue;
        }

        if (properties.TryGetValue("Font", out object fontValue))
            Font = (string)fontValue;

        if (properties.TryGetValue("ColorIndex", out object colorIndexValue))
            ColorIndex = (int)colorIndexValue;
    }
}
