namespace Application.Common.Exceptions;

public class AlreadyCreatedException : Exception
{
    public AlreadyCreatedException() : base() { }
    public AlreadyCreatedException(string message) : base(message) { }
    public AlreadyCreatedException(string message, Exception inner) : base(message, inner) { }
}