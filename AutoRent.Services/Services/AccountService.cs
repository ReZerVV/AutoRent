using System;
using AutoRent.Domain;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using AutoRent.Data;
using AutoRent.Services.DTOs;
namespace AutoRent.Services;

public class AccountService : IAccountService
{
    private readonly AppDataContext _context;
    public AccountService(AppDataContext context)
    {
        _context = context;
    }

    public AccountDto Register(AccountRegisterDto registerData)
    {
        if (string.IsNullOrEmpty(registerData.Username) ||
            string.IsNullOrEmpty(registerData.Password))
        {
            throw new ValidationException("The username or password is incorrect. Please fix it and try again.");
        }
        if (_context.Accounts
            .FirstOrDefault(userCredentials => userCredentials.Username == registerData.Username) != null)
        {
            throw new InvalidCredentialsException("The user is already registered. Please login.");
        }
        var userCredentials = new Account
        {
            Username = registerData.Username,
            Password = registerData.Password,
            Role = registerData.Role,
        };
        _context.Accounts.Add(userCredentials);
        _context.SaveChanges();
        return AccountDto.FromEntity(userCredentials);
    }

    public AccountDto Login(AccountLoginDto loginData)
    {
        if (string.IsNullOrEmpty(loginData.Username) ||
            string.IsNullOrEmpty(loginData.Password))
        {
            throw new ValidationException("The username or password is incorrect. Please fix it and try again.");
        }
        var userCredentials = _context.Accounts
            .FirstOrDefault(userCredentials => userCredentials.Username == loginData.Username);
        if (userCredentials == null ||
            userCredentials.Password != loginData.Password)
        {
            throw new InvalidCredentialsException("Invalid user credentials.");
        }
        return AccountDto.FromEntity(userCredentials);
    }
}