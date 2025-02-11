using System.Runtime.Serialization;

namespace CoreLib.Base;

[Serializable]
public class FileReadException : Exception
{
    public FileReadException() : base() { }
    public FileReadException(string message) : base(message) { }
    public FileReadException(string message, Exception inner) : base(message, inner) { }
    protected FileReadException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}