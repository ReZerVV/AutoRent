using AutoRent.Domain;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using AutoRent.Services.DTOs;
using AutoRent.Services.Repositories;

namespace AutoRent.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public AccountDto Register(AccountRegisterDto registerData)
    {
        if (string.IsNullOrEmpty(registerData.Username) ||
            string.IsNullOrEmpty(registerData.Password))
        {
            throw new ValidationException("The username or password is incorrect. Please fix it and try again.");
        }
        if (accountRepository.GetByUsername(registerData.Username) != null)
        {
            throw new InvalidCredentialsException("The user is already registered. Please login.");
        }
        var userCredentials = new Account
        {
            Username = registerData.Username,
            Password = registerData.Password,
            Role = registerData.Role,
        };
        accountRepository.Create(userCredentials);
        accountRepository.SaveChanges();
        return AccountDto.FromEntity(userCredentials);
    }

    public AccountDto Login(AccountLoginDto loginData)
    {
        if (string.IsNullOrEmpty(loginData.Username) ||
            string.IsNullOrEmpty(loginData.Password))
        {
            throw new ValidationException("The username or password is incorrect. Please fix it and try again.");
        }
        var userCredentials = accountRepository.GetByUsername(loginData.Username);
        if (userCredentials == null ||
            userCredentials.Password != loginData.Password)
        {
            throw new InvalidCredentialsException("Invalid user credentials.");
        }
        return AccountDto.FromEntity(userCredentials);
    }
}