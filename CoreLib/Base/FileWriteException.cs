using System.Runtime.Serialization;

namespace CoreLib.Base;

[Serializable]
public class FileWriteException : Exception
{
    public FileWriteException() : base() { }
    public FileWriteException(string message) : base(message) { }
    public FileWriteException(string message, Exception inner) : base(message, inner) { }
    protected FileWriteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}