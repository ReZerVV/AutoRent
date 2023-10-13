using Microsoft.EntityFrameworkCore;
using AutoRent.Data;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.DTOs;
namespace AutoRent.Tests;

[TestClass]
public class AccountTest
{
    [TestMethod]
    public void TestRegister_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestRegister_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            var accountService = new AccountService(context);
            var account = accountService.Register(new AccountRegisterDto
            {
                Username = "TEST",
                Password = "TEST",
                Role = AccountRole.Customer,
            });
        }
    }
    
    [TestMethod]
    public void TestLogin_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "AutoRentDbForTestLogin_1")
                .Options;
        using (var context = new AppDataContext(options))
        {
            context.Accounts.AddRange(
                new Account
                {
                    Id = 1,
                    Username = "TEST",
                    Password = "TEST",
                    Role = AccountRole.Customer,
                }
            );
            context.SaveChanges();
            var accountService = new AccountService(context);
            var account = accountService.Login(new AccountLoginDto
            {
                Username = "TEST",
                Password = "TEST",
            });
        }
    }
}