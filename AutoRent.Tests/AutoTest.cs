using AutoRent.Data;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Tests;

[TestClass]
public class AutoTest
{
    [TestMethod]
    public void TestCreateAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestCreateAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            var autoService = new AutoService(context);
            autoService.Create(new AutoCreateDto
            {
                Name = "TEST",
                Description = "TEST",
                Class = "TEST",
                Brand = "TEST",
                CostPerHour = 0,
            });
        }
    }
    
    [TestMethod]
    public void TestDeleteAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestDeleteAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
            var autoService = new AutoService(context);
            autoService.Delete(1);
            Assert.IsNull(context.Autos.Find(1));
        }
    }
    
    [TestMethod]
    public void TestUpdateAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestUpdateAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
            var autoService = new AutoService(context);
            autoService.Update(new AutoUpdateDto
            {
                Id = 1,
                Name = "TEST_UPDATED",
                Description = "TEST_UPDATED",
                Class = "TEST_UPDATED",
                Brand = "TEST_UPDATED",
                CostPerHour = 0,
            });
            var auto = context.Autos.Find(1);
            Assert.IsNotNull(auto);
            Assert.AreEqual("TEST_UPDATED", auto.Name);
            Assert.AreEqual("TEST_UPDATED", auto.Description);
            Assert.AreEqual("TEST_UPDATED", auto.Class);
            Assert.AreEqual("TEST_UPDATED", auto.Brand);
        }
    }
    
    [TestMethod]
    public void TestSetLocationAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestSetLocationAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
            var autoService = new AutoService(context);
            autoService.SetLocation(new AutoSetLocationDto
            {
                Id = 1,
                Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
            });
            var auto = context.Autos.Find(1);
            Assert.IsNotNull(auto);
            Assert.AreEqual("Ukraine,Zaporizhzhia region,Zaporizhzhia", auto.Location);
        }
    }

    [TestMethod]
    public void TestGetRentalAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestGetRentalAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
            var autoService = new AutoService(context);
            var autos = autoService.GetRentalAutos();
            CollectionAssert.AreEqual(new AutoDto[0], autos.ToArray());
        }
    }
    
    [TestMethod]
    public void TestSearchAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestSearchAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
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
            var autoService = new AutoService(context);
            var autos = autoService.Search(new AutoSearchDto
            {
                KeyWords = "TEST",
            });
            CollectionAssert.AreEqual(new[] { 1 }, autos.Select(auto => auto.Id).ToArray());
        }
    }
    
    [TestMethod]
    public void TestSearchAuto_2()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestSearchAuto_2")
            .Options;
        using (var context = new AppDataContext(options))
        {
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoService = new AutoService(context);
            var autos = autoService.Search(new AutoSearchDto
            {
                Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
            });
            CollectionAssert.AreEqual(new[] { 1 }, autos.Select(auto => auto.Id).ToArray());
        }
    }
    
    [TestMethod]
    public void TestSearchAuto_3()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestSearchAuto_3")
            .Options;
        using (var context = new AppDataContext(options))
        {
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoService = new AutoService(context);
            var autos = autoService.Search(new AutoSearchDto
            {
                 Class = "TEST",
            });
            CollectionAssert.AreEqual(new[] { 1 }, autos.Select(auto => auto.Id).ToArray());
        }
    }
    
    [TestMethod]
    public void TestSearchAuto_4()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestSearchAuto_4")
            .Options;
        using (var context = new AppDataContext(options))
        {
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoService = new AutoService(context);
            var autos = autoService.Search(new AutoSearchDto
            {
                Brand = "TEST",
            });
            CollectionAssert.AreEqual(new[] { 1 }, autos.Select(auto => auto.Id).ToArray());
        }
    }
    
    [TestMethod]
    public void TestSearchAuto_5()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestSearchAuto_5")
            .Options;
        using (var context = new AppDataContext(options))
        {
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoService = new AutoService(context);
            var autos = autoService.Search(new AutoSearchDto
            {
                MinCost = 0,
                MaxCost = 100,
            });
            CollectionAssert.AreEqual(new[] { 1 }, autos.Select(auto => auto.Id).ToArray());
        }
    }
    
    [TestMethod]
    public void TestGetAllAutoClasses_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestGetAllAutoClasses_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoService = new AutoService(context);
            var classes = autoService.GetAllAutoClasses();
            CollectionAssert.AreEqual(new[] { "TEST" }, classes.ToArray());
        }
    }
    
    [TestMethod]
    public void TestGetAllAutoBrands_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestTestGetAllAutoBrands_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            context.Autos.AddRange(
                new Auto()
                {
                    Id = 1,
                    Name = "TEST",
                    Description = "TEST",
                    Class = "TEST",
                    Brand = "TEST",
                    Location = "Ukraine,Zaporizhzhia region,Zaporizhzhia",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoService = new AutoService(context);
            var classes = autoService.GetAllAutoBrands();
            CollectionAssert.AreEqual(new[] { "TEST" }, classes.ToArray());
        }
    }
}