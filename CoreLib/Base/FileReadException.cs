using System.Runtime.Serialization;

namespace CoreLib.Base;

[Serializable]
public class CustomException : Exception
{
    public CustomException() : base() { }
    
    public CustomException(string message) : base(message) { }
    
    public CustomException(string message, Exception inner) : base(message, inner) { }
    
    protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}