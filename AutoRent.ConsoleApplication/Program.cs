using AutoRent.Dal;
using AutoRent.Dal.Repositories;
using AutoRent.Domain;
using AutoRent.Services;
using AutoRent.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static IAccountService accountService;
    private static IAutoService autoService;
    private static IRentService rentService;
    private static bool IsOpen;

    public static void Main(string[] argv)
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseInMemoryDatabase(databaseName: "AutoRentDbForTestGetAllAutoClasses_1")
            .Options;
        using (var context = new AppDataContext(options)) 
        {
            {
                var accountRepository = new AccountRepository(context);
                var autoRepository = new AutoRepository(context);
                var rentRepository = new RentRepository(context);
                accountService = new AccountService(accountRepository);
                autoService = new AutoService(autoRepository);
                rentService = new RentService(
                    rentRepository,
                    accountRepository,
                    autoRepository);
                IsOpen = true;
            }
            while (IsOpen)
            {
                Console.Write("command: ");
                switch(Console.ReadLine().ToUpper()) 
                {
                    case "ACCOUNT LOGIN":
                        Console.Clear();
                        Console.WriteLine("ACCOUNT LOGIN");
                        {
                            Console.Write("username:");
                            var username = Console.ReadLine();
                            Console.Write("password:");
                            var password = Console.ReadLine();
                            try
                            {
                                accountService.Login(new AutoRent.Services.DTOs.AccountLoginDto
                                {
                                    Username = username,
                                    Password = password,
                                });
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "ACCOUNT REGISTER":
                        Console.Clear();
                        Console.WriteLine("ACCOUNT REGISTER");
                        {
                            Console.Write("username:");
                            var username = Console.ReadLine();
                            Console.Write("password:");
                            var password = Console.ReadLine();
                            Console.Write("role[Administrator, Manager, Customer]:");
                            AccountRole role;
                            while (!Enum.TryParse<AccountRole>(Console.ReadLine(), out role)) 
                            {
                                Console.WriteLine("Invalid role");
                            };
                            try
                            {
                                accountService.Register(new AutoRent.Services.DTOs.AccountRegisterDto
                                {
                                    Username = username,
                                    Password = password,
                                    Role = role,
                                });
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO CREATE":
                        Console.Clear();
                        Console.WriteLine("AUTO CREATE");
                        {
                            Console.Write("name:");
                            var name = Console.ReadLine();
                            Console.Write("description:");
                            var description = Console.ReadLine();
                            Console.Write("brand:");
                            var brand = Console.ReadLine();
                            Console.Write("class:");
                            var car_class = Console.ReadLine();
                            Console.Write("cost per hour:");
                            decimal costPerHour = 0;
                            while (!decimal.TryParse(Console.ReadLine(), out costPerHour))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            try
                            {
                                autoService.Create(new AutoRent.Services.DTOs.AutoCreateDto
                                {
                                    Name = name,
                                    Description = description,
                                    Brand = brand,
                                    Class = car_class,
                                    CostPerHour = costPerHour,
                                });
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO DELETE":
                        Console.Clear();
                        Console.WriteLine("AUTO DELETE");
                        {
                            Console.Write("id:");
                            int id;
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            try
                            {
                                autoService.Delete(id);
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO EDIT":
                        Console.Clear();
                        Console.WriteLine("AUTO EDIT");
                        {
                            Console.Write("id:");
                            int id;
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            Console.Write("name:");
                            var name = Console.ReadLine();
                            Console.Write("description:");
                            var description = Console.ReadLine();
                            Console.Write("brand:");
                            var brand = Console.ReadLine();
                            Console.Write("class:");
                            var car_class = Console.ReadLine();
                            Console.Write("cost per hour:");
                            decimal costPerHour = 0;
                            while (!decimal.TryParse(Console.ReadLine(), out costPerHour))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            try
                            {
                                autoService.Update(new AutoRent.Services.DTOs.AutoUpdateDto 
                                {
                                    Id = id,
                                    Name = name,
                                    Description = description,
                                    Brand = brand,
                                    Class = car_class,
                                    CostPerHour = costPerHour,
                                });
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO SET LOCATION":
                        Console.Clear();
                        Console.WriteLine("AUTO SET LOCATION");
                        {
                            Console.Write("id:");
                            int id;
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            Console.Write("location:");
                            var location = Console.ReadLine();
                            try
                            {
                                autoService.SetLocation(new AutoRent.Services.DTOs.AutoSetLocationDto
                                {
                                    Id = id,
                                    Location = location,
                                });
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO GET ALL":
                        Console.Clear();
                        Console.WriteLine("AUTO GET ALL");
                        {
                            foreach (var auto in autoService.GetAllAutos())
                            {
                                Console.Write($"[{auto.Id}] {auto.Name} {auto.Brand} {auto.Class} {auto.Description} {auto.CostPerHour}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO GET RENTAL":
                        Console.Clear();
                        Console.WriteLine("AUTO GET RENTAL");
                        {
                            foreach (var auto in autoService.GetRentalAutos())
                            {
                                Console.Write($"[{auto.Id}] {auto.Name} {auto.Brand} {auto.Class} {auto.Description} {auto.CostPerHour}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO SEARCH":
                        Console.Clear();
                        Console.WriteLine("AUTO SEARCH");
                        {
                            Console.Write("keyWords:");
                            var keyWords = Console.ReadLine();
                            Console.Write("location:");
                            var location = Console.ReadLine();
                            Console.Write("brand:"); 
                            var brand = Console.ReadLine();
                            Console.Write("class:");
                            var car_class = Console.ReadLine();
                            Console.Write("min cost:");
                            int min_cost;
                            while (!int.TryParse(Console.ReadLine(), out min_cost))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            Console.Write("max cost:");
                            int max_cost;
                            while (!int.TryParse(Console.ReadLine(), out max_cost))
                            {
                                Console.WriteLine("Invalid number format");
                            }
                            try
                            {
                                var autos = autoService.Search(new AutoRent.Services.DTOs.AutoSearchDto
                                {
                                    KeyWords = keyWords,
                                    Location = location,
                                    Brand = brand,
                                    Class = car_class,
                                    MinCost = min_cost,
                                    MaxCost = max_cost
                                });
                                foreach (var auto in autos)
                                {
                                    Console.Write($"[{auto.Id}] {auto.Name} {auto.Brand} {auto.Class} {auto.Description} {auto.CostPerHour}");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO GET BRANDS":
                        Console.Clear();
                        Console.WriteLine("AUTO GET BRANDS");
                        {
                            foreach (var brand in autoService.GetAllAutoBrands())
                            {
                                Console.Write(brand);
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "AUTO GET CLASSES":
                        Console.Clear();
                        Console.WriteLine("AUTO GET CLASSES");
                        {
                            foreach (var car_class in autoService.GetAllAutoClasses())
                            {
                                Console.Write(car_class);
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "RENT BOOKING":
                        Console.Clear();
                        Console.WriteLine("RENT BOOKING");
                        {
                            try
                            {
                                Console.Write("renter id:");
                                int renter_id;
                                while (!int.TryParse(Console.ReadLine(), out renter_id))
                                {
                                    Console.WriteLine("Invalid number format");
                                }
                                Console.Write("auto id:");
                                int auto_id;
                                while (!int.TryParse(Console.ReadLine(), out auto_id))
                                {
                                    Console.WriteLine("Invalid number format");
                                }
                                Console.Write("rental start date:");
                                DateTime rental_start_date;
                                while (!DateTime.TryParse(Console.ReadLine(), out rental_start_date))
                                {
                                    Console.WriteLine("Invalid date time format");
                                }
                                Console.Write("rental end date:");
                                DateTime rental_end_date;
                                while (!DateTime.TryParse(Console.ReadLine(), out rental_end_date))
                                {
                                    Console.WriteLine("Invalid date time format");
                                }
                                rentService.Booking(new AutoRent.Services.DTOs.AutoRentOrderBookingDto
                                {
                                    RenterId = renter_id,
                                    AutoId = auto_id,
                                    RentalStartDate = rental_start_date,
                                    RentalEndDate = rental_end_date,
                                });
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "RENT RENT":
                        Console.Clear();
                        Console.WriteLine("RENT RENT");
                        {
                            try
                            {
                                Console.Write("rent order id:");
                                int order_id;
                                while (!int.TryParse(Console.ReadLine(), out order_id))
                                {
                                    Console.WriteLine("Invalid number format");
                                }
                                rentService.Rent(order_id);
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "RENT RETURN":
                        Console.Clear();
                        Console.WriteLine("RENT RETURN");
                        {
                            try
                            {
                                Console.Write("rent order id:");
                                int order_id;
                                while (!int.TryParse(Console.ReadLine(), out order_id))
                                {
                                    Console.WriteLine("Invalid number format");
                                }
                                rentService.Return(order_id);
                                Console.WriteLine($"Successful");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "RENT GET HISTORY":
                        Console.Clear();
                        Console.WriteLine("RENT GET HISTORY");
                        {
                            try
                            {
                                Console.Write("id:");
                                int id;
                                while (!int.TryParse(Console.ReadLine(), out id))
                                {
                                    Console.WriteLine("Invalid number format");
                                }
                                var histories = rentService.GetHistoryByRenterId(id);
                                foreach (var history in histories)
                                {
                                    Console.WriteLine($"[{history.Id}] {history.RentalStartDate}-{history.RentalEndDate} {history.Auto.Name} {history.Cost}$");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "RENT GET ORDERS BY AUTO ID":
                        Console.Clear();
                        Console.WriteLine("RENT GET ORDERS BY AUTO ID");
                        {
                            try
                            {
                                Console.Write("id:");
                                int id;
                                while (!int.TryParse(Console.ReadLine(), out id))
                                {
                                    Console.WriteLine("Invalid number format");
                                }
                                var histories = rentService.GetOrdersByAutoId(id);
                                foreach (var history in histories)
                                {
                                    Console.WriteLine($"[{history.Id}] {history.RentalStartDate}-{history.RentalEndDate} {history.Renter.Username}");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Error: {e.Message}");
                            }
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    case "EXIT":
                        IsOpen = false;
                        break;

                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }
        }
    }
}