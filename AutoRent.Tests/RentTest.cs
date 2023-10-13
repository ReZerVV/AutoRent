using AutoRent.Data;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Tests;

[TestClass]
public class RentTest
{
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
            var rentService = new RentService(context);
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
            var rentService = new RentService(context);
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
            var rentService = new RentService(context);
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
                    RentalStartDate = DateTime.Now,
                    RentalEndDate = DateTime.Now.AddHours(2),
                    Status = AutoRentOrderStatus.Progress,
                }
            );
            context.SaveChanges();
            var rentService = new RentService(context);
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
            var rentService = new RentService(context);
            var orders = rentService.GetHistoryByRenterId(1);
            CollectionAssert.AreEqual(new[] { 1 }, orders.Select(order => order.Id).ToList());
        }
    }
}