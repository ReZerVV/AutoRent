using AutoRent.Dal;
using AutoRent.Dal.Repositories;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Tests;

[TestClass]
public class AutoTest
{
    [TestMethod]
    public void TestRepositoryCreate_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
           .UseInMemoryDatabase(databaseName: "Db_1")
           .Options;
        using (var context = new AppDataContext(options))
        {
            var autoRepository = new AutoRepository(context);
            var auto = new Auto
            {
                Name = "TEST",
                Description = "TEST",
                Brand = "TEST",
                Class = "TEST",
                CostPerHour = 0,
            };
            autoRepository.Create(auto);
            autoRepository.SaveChanges();
            Assert.IsNotNull(context.Autos.Find(auto.Id));
        }
    }

    [TestMethod]
    public void TestRepositoryRemove_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
           .UseInMemoryDatabase(databaseName: "Db_2")
           .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Autos.AddRange(
                    new Auto
                    {
                        Name = "TEST",
                        Description = "TEST",
                        Brand = "TEST",
                        Class = "TEST",
                        CostPerHour = 0,
                    }
                );
                context.SaveChanges();
            }
            var autoRepository = new AutoRepository(context);
            autoRepository.Remove(context.Autos.Find(1));
            autoRepository.SaveChanges();
            Assert.IsNull(context.Accounts.Find(1));
        }
    }

    [TestMethod]
    public void TestRepositoryGetById_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
           .UseInMemoryDatabase(databaseName: "Db_3")
           .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Autos.AddRange(
                    new Auto
                    {
                        Name = "TEST",
                        Description = "TEST",
                        Brand = "TEST",
                        Class = "TEST",
                        CostPerHour = 0,
                    }
                );
                context.SaveChanges();
            }
            var autoRepository = new AutoRepository(context);
            Assert.IsNotNull(autoRepository.GetById(1));
        }
    }

    [TestMethod]
    public void TestRepositoryGetAll_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
           .UseInMemoryDatabase(databaseName: "Db_4")
           .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Autos.AddRange(
                    new Auto
                    {
                        Name = "TEST",
                        Description = "TEST",
                        Brand = "TEST",
                        Class = "TEST",
                        CostPerHour = 0,
                    }
                );
                context.SaveChanges();
            }
            var autoRepository = new AutoRepository(context);
            CollectionAssert.AreEqual(new[] { 1,}, autoRepository.GetAll().Select(auto => auto.Id).ToList());
        }
    }

    [TestMethod]
    public void TestRepositoryGetBrands_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
           .UseInMemoryDatabase(databaseName: "Db_5")
           .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Autos.AddRange(
                    new Auto
                    {
                        Name = "TEST",
                        Description = "TEST",
                        Brand = "TEST",
                        Class = "TEST",
                        CostPerHour = 0,
                    }
                );
                context.SaveChanges();
            }
            var autoRepository = new AutoRepository(context);
            CollectionAssert.AreEqual(new[] { "TEST", }, autoRepository.GetAllBrands().ToList());
        }
    }

    [TestMethod]
    public void TestRepositoryGetClasses_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
           .UseInMemoryDatabase(databaseName: "Db_6")
           .Options;
        using (var context = new AppDataContext(options))
        {
            {
                context.Autos.AddRange(
                    new Auto
                    {
                        Name = "TEST",
                        Description = "TEST",
                        Brand = "TEST",
                        Class = "TEST",
                        CostPerHour = 0,
                    }
                );
                context.SaveChanges();
            }
            var autoRepository = new AutoRepository(context);
            CollectionAssert.AreEqual(new[] { "TEST", }, autoRepository.GetAllClasses().ToList());
        }
    }

    [TestMethod]
    public void TestCreateAuto_1()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestCreateAuto_1")
            .Options;
        using (var context = new AppDataContext(options))
        {
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
            autoService.SetLocation(new AutoSetLocationDto
            {
                Id = 1,
                Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
            });
            var auto = context.Autos.Find(1);
            Assert.IsNotNull(auto);
            Assert.AreEqual("UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1", auto.Location);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 0,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
            var autos = autoService.Search(new AutoSearchDto
            {
                Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
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
                    Location = "UA,Zaporizhzhia region,Zaporizhzhia,Schvchenko,1",
                    CostPerHour = 10,
                }
            );
            context.SaveChanges();
            var autoRepository = new AutoRepository(context);
            var autoService = new AutoService(autoRepository);
            var classes = autoService.GetAllAutoBrands();
            CollectionAssert.AreEqual(new[] { "TEST" }, classes.ToArray());
        }
    }
}