namespace SharedLibrary.Components.Modal.Models;

public sealed class ModalResult
{
    public bool Confirmed { get; set; }
    
    public object? Data { get; set; }
    
    public Type? DataType { get; set; }
    
    public string? ErrorMessage { get; private init; }

    public ModalResult(bool confirmed, object? data = null, string? errorMessage = null)
    {
        Confirmed = confirmed;
        Data = data;
        DataType = data?.GetType();
        ErrorMessage = errorMessage;
    }

    public static ModalResult Ok() => new(true);
    
    public static ModalResult Ok<T>(T data) => new (true, data);
    
    public static ModalResult Cancel() => new(false);
    
    public static ModalResult Error(string errorMessage) => new(false, errorMessage: errorMessage);

    // 安全数据转换
    public T GetData<T>()
    {
        if(Data is T data) return data;
        if(Data == null) return default!;
        throw new InvalidOperationException($"类型不匹配：{DataType} => {typeof(T)}");
    }
    
    // 模式匹配支持
    public void Deconstruct(out bool confirmed, out object? data)
        => (confirmed, data) = (Confirmed, Data);
}