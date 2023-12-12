namespace TODOApp.Api.Exceptions;

[System.Serializable]
public class TODOAPPApiBaseException : Exception
{
    public TODOAPPApiBaseException() { }
    public TODOAPPApiBaseException(string message) : base(message) { }
    public TODOAPPApiBaseException(string message, Exception inner) : base(message, inner) { }
    protected TODOAPPApiBaseException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}