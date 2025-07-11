﻿namespace Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }
    
    public ForbiddenAccessException(string message) : base(message) { }
    
    public ForbiddenAccessException(string message, Exception inner) : base(message, inner) { }
}