namespace Application.Common.Exceptions;

public class AlreadyRevokeException : Exception
{
    public AlreadyRevokeException() : base() { }
    public AlreadyRevokeException(string message) : base(message) { }
    public AlreadyRevokeException(string message, Exception inner) : base(message, inner) { }
}