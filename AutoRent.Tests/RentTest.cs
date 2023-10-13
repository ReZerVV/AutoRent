using AutoRent.Dal;
using AutoRent.Dal.Repositories;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Tests;

[TestClass]
public class RentTest
{
    [TestMethod]
    public void TestRepositoryCreate_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRent_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
                context.Autos.AddRange(
                    new Auto()
                    {
                        Id = 1,
                        Name = "TEST",
                        Description = "TEST",
                        Class = "TEST",
                        Brand = "TEST",
                        Location = string.Empty,
                        CostPerHour = 0,
                    }
                );
                context.SaveChanges();
            }
            var rentRepository = new RentRepository(context);
            var rentOrder = new AutoRentOrder
            {
                RenterId = 0,
                AutoId = 0,
                Status = AutoRentOrderStatus.Сompleted,
                RentalStartDate = DateTime.Now,
                RentalEndDate = DateTime.Now,
                Cost = 0,
            };
            rentRepository.Create(rentOrder);
            Assert.IsNotNull(context.Orders.Find(rentOrder.Id));
        }
    }

    [TestMethod]
    public void TestRepositoryGetById_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRent_2")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
                context.Autos.AddRange(
                    new Auto()
                    {
                        Id = 1,
                        Name = "TEST",
                        Description = "TEST",
                        Class = "TEST",
                        Brand = "TEST",
                        Location = string.Empty,
                        CostPerHour = 0,
                    }
                );
                context.Orders.AddRange(
                    new AutoRentOrder
                    {
                        Id = 1,
                        RenterId = 1,
                        AutoId = 1,
                        RentalStartDate = DateTime.Now,
                        RentalEndDate = DateTime.Now.AddHours(2),
                        Status = AutoRentOrderStatus.Waiting,
                    }
                );
                context.SaveChanges();
            }
            var rentRepository = new RentRepository(context);
            Assert.IsNotNull(rentRepository.GetById(1));
        }
    }

    [TestMethod]
    public void TestRepositoryGetAll_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRent_3")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
                context.Autos.AddRange(
                    new Auto()
                    {
                        Id = 1,
                        Name = "TEST",
                        Description = "TEST",
                        Class = "TEST",
                        Brand = "TEST",
                        Location = string.Empty,
                        CostPerHour = 0,
                    }
                );
                context.Orders.AddRange(
                    new AutoRentOrder
                    {
                        Id = 1,
                        RenterId = 1,
                        AutoId = 1,
                        RentalStartDate = DateTime.Now,
                        RentalEndDate = DateTime.Now.AddHours(2),
                        Status = AutoRentOrderStatus.Waiting,
                    }
                );
                context.SaveChanges();
            }
            var rentRepository = new RentRepository(context);
            CollectionAssert.AreEqual(new[] { 1, }, rentRepository.GetAll().Select(order => order.Id).ToList());
        }
    }


    [TestMethod]
    public void TestRepositoryGetOrdersByAutoId_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRent_4")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
                context.Autos.AddRange(
                    new Auto()
                    {
                        Id = 1,
                        Name = "TEST",
                        Description = "TEST",
                        Class = "TEST",
                        Brand = "TEST",
                        Location = string.Empty,
                        CostPerHour = 0,
                    }
                );
                context.Orders.AddRange(
                    new AutoRentOrder
                    {
                        Id = 1,
                        RenterId = 1,
                        AutoId = 1,
                        RentalStartDate = DateTime.Now.AddMinutes(2),
                        RentalEndDate = DateTime.Now.AddHours(2),
                        Status = AutoRentOrderStatus.Waiting,
                    }
                );
                context.SaveChanges();
            }
            var rentRepository = new RentRepository(context);
            CollectionAssert.AreEqual(new[] { 1, }, rentRepository.GetOrdersByAutoId(1).Select(order => order.Id).ToList());
        }
    }

    [TestMethod]
    public void TestRepositoryGetHistoryByRenterId_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "DbRent_5")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
                context.Autos.AddRange(
                    new Auto()
                    {
                        Id = 1,
                        Name = "TEST",
                        Description = "TEST",
                        Class = "TEST",
                        Brand = "TEST",
                        Location = string.Empty,
                        CostPerHour = 0,
                    }
                );
                context.Orders.AddRange(
                    new AutoRentOrder
                    {
                        Id = 1,
                        RenterId = 1,
                        AutoId = 1,
                        RentalStartDate = DateTime.Now,
                        RentalEndDate = DateTime.Now.AddHours(2),
                        Status = AutoRentOrderStatus.Сompleted,
                    }
                );
                context.SaveChanges();
            }
            var rentRepository = new RentRepository(context);
            CollectionAssert.AreEqual(new[] { 1, }, rentRepository.GetHistoryByRenterId(1).Select(order => order.Id).ToList());
        }
    }

    [TestMethod]
    public void TestBookingAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestBookingAuto_1")
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
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = string.Empty,
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var accountRepositry = new AccountRepository(context);
            var autoRepository = new AutoRepository(context);
            var rentRepository = new RentRepository(context);
            var rentService = new RentService(
                rentRepository,
                accountRepositry,
                autoRepository);
            rentService.Booking(new AutoRentOrderBookingDto
            {
                RenterId = 1,
                AutoId = 1,
                RentalStartDate = DateTime.Now,
                RentalEndDate = DateTime.Now.AddHours(2)
            });
            var autoRentOrder = context.Orders.Find(1);
            Assert.AreEqual(AutoRentOrderStatus.Waiting, autoRentOrder.Status);
        }
    }
    
    [TestMethod]
    public void TestRentAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestRentAuto_1")
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
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = string.Empty,
                    CostPerHour = 0,
                }
            );
            context.Orders.AddRange(
                new AutoRentOrder
                {
                    Id = 1,
                    RenterId = 1,
                    AutoId = 1,
                    RentalStartDate = DateTime.Now,
                    RentalEndDate = DateTime.Now.AddHours(2),
                    Status = AutoRentOrderStatus.Waiting,
                }
            );
            context.SaveChanges(); 
            var accountRepositry = new AccountRepository(context);
            var autoRepository = new AutoRepository(context);
            var rentRepository = new RentRepository(context);
            var rentService = new RentService(
                rentRepository,
                accountRepositry,
                autoRepository);
            rentService.Rent(1);
            var autoRentOrder = context.Orders.Find(1);
            Assert.AreEqual(AutoRentOrderStatus.Progress, autoRentOrder.Status);
        }
    }
    
    [TestMethod]
    public void TestReturnAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestReturnAuto_1")
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
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = string.Empty,
                    CostPerHour = 0,
                }
            );
            context.Orders.AddRange(
                new AutoRentOrder
                {
                    Id = 1,
                    RenterId = 1,
                    AutoId = 1,
                    RentalStartDate = DateTime.Now,
                    RentalEndDate = DateTime.Now.AddHours(2),
                    Status = AutoRentOrderStatus.Progress,
                }
            );
            context.SaveChanges();
            var accountRepositry = new AccountRepository(context);
            var autoRepository = new AutoRepository(context);
            var rentRepository = new RentRepository(context);
            var rentService = new RentService(
                rentRepository,
                accountRepositry,
                autoRepository);
            rentService.Return(1);
            var autoRentOrder = context.Orders.Find(1);
            Assert.AreEqual(AutoRentOrderStatus.Сompleted, autoRentOrder.Status);
        }
    }
    
    [TestMethod]
    public void TestGetOrdersByAutoId_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestGetOrdersByAutoId_1")
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
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = string.Empty,
                    CostPerHour = 0,
                }
            );
            context.Orders.AddRange(
                new AutoRentOrder
                {
                    Id = 1,
                    RenterId = 1,
                    AutoId = 1,
                    RentalStartDate = DateTime.Now.AddMinutes(2),
                    RentalEndDate = DateTime.Now.AddHours(2),
                    Status = AutoRentOrderStatus.Waiting,
                }
            );
            context.SaveChanges();
            var accountRepositry = new AccountRepository(context);
            var autoRepository = new AutoRepository(context);
            var rentRepository = new RentRepository(context);
            var rentService = new RentService(
                rentRepository,
                accountRepositry,
                autoRepository);
            var orders = rentService.GetOrdersByAutoId(1);
            CollectionAssert.AreEqual(new[] { 1 }, orders.Select(order => order.Id).ToList());
        }
    }
    
    [TestMethod]
    public void TestGetHistoryByRenterId_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestGetHistoryByRenterId_1")
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
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = string.Empty,
                    CostPerHour = 0,
                }
            );
            context.Orders.AddRange(
                new AutoRentOrder
                {
                    Id = 1,
                    RenterId = 1,
                    AutoId = 1,
                    RentalStartDate = DateTime.Now,
                    RentalEndDate = DateTime.Now.AddHours(2),
                    Status = AutoRentOrderStatus.Сompleted,
                }
            );
            context.SaveChanges();
            var accountRepositry = new AccountRepository(context);
            var autoRepository = new AutoRepository(context);
            var rentRepository = new RentRepository(context);
            var rentService = new RentService(
                rentRepository,
                accountRepositry,
                autoRepository);
            var orders = rentService.GetHistoryByRenterId(1);
            CollectionAssert.AreEqual(new[] { 1 }, orders.Select(order => order.Id).ToList());
        }
    }
}