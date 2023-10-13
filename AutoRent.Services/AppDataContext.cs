using AutoRent.Domain;
using Microsoft.EntityFrameworkCore;
namespace AutoRent.Data;

public class AppDataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Auto> Autos { get; set; }
    public DbSet<AutoRentOrder> Orders { get; set; }

    public AppDataContext(DbContextOptions options)
        : base(options)
    {
        
    }
}