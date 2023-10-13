using Microsoft.EntityFrameworkCore;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.DTOs;
using AutoRent.Dal;
using AutoRent.Dal.Repositories;

namespace AutoRent.Tests;

[TestClass]
public class AccountTest
{
    [TestMethod]
    public void TestRepositoryCreate_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRepository_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            var accountRepository = new AccountRepository(context);
            var accountCreadentials = new Account
            {
                Username = "TEST",
                Password = "TEST",
                Role = AccountRole.Customer,
            };
            accountRepository.Create(accountCreadentials);
            accountRepository.SaveChanges();
            Assert.IsNotNull(context.Accounts.Find(accountCreadentials.Id));
        }
    }

    [TestMethod]
    public void TestRepositoryGetById_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRepository_2")
            .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Accounts.AddRange(
                    new Account
                    {
                        Username = "TEST",
                        Password = "TEST",
                        Role = AccountRole.Customer,
                    }
                );
                context.SaveChanges();
            }
            var accountRepository = new AccountRepository(context);
            Assert.IsNotNull(accountRepository.GetById(1));
        }
    }

    [TestMethod]
    public void TestRepositoryGetByUsername_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRepository_3")
            .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Accounts.AddRange(
                    new Account
                    {
                        Username = "TEST",
                        Password = "TEST",
                        Role = AccountRole.Customer,
                    }
                );
                context.SaveChanges();
            }
            var accountRepository = new AccountRepository(context);
            Assert.IsNotNull(accountRepository.GetByUsername("TEST"));
        }
    }

    [TestMethod]
    public void TestRegister_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestRegister_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            var accountRepository = new AccountRepository(context);
            var accountService = new AccountService(accountRepository);
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
            var accountRepository = new AccountRepository(context);
            var accountService = new AccountService(accountRepository);
            var account = accountService.Login(new AccountLoginDto
            {
                Username = "TEST",
                Password = "TEST",
            });
        }
    }
}