using AutoRent.Domain;
namespace AutoRent.Services.DTOs;

public class AccountRegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public AccountRole Role { get; set; }
}