using System;
namespace AutoRent.Services.Exceptions;

public class InvalidCredentialsException : Exception 
{
    public InvalidCredentialsException(string message)
        : base(message)
    {
        
    }
}