using System;
namespace AutoRent.Services.Exceptions;

public class ValidationException : Exception 
{
    public ValidationException(string message)
        : base(message)
    {
        
    }
}