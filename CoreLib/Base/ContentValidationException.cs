using System.Runtime.Serialization;

namespace CoreLib.Base;

[Serializable]

public class ContentValidationException : Exception
{
    public ContentValidationException() : base() { }
    public ContentValidationException(string message) : base(message) { }
    public ContentValidationException(string message, Exception inner) : base(message, inner) { }
    protected ContentValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}