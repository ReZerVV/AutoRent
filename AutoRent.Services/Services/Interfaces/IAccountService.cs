using AutoRent.Services.DTOs;
namespace AutoRent.Services.Interfaces;

public interface IAccountService
{
    AccountDto Register(AccountRegisterDto registerData);
    AccountDto Login(AccountLoginDto loginData);
}