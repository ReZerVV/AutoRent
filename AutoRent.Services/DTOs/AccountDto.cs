using AutoRent.Domain;
namespace AutoRent.Services.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public AccountRole Role { get; set; }

    public static AccountDto FromEntity(Account entity)
    {
        return new AccountDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Password = entity.Password,
            Role = entity.Role,
        };
    }
}