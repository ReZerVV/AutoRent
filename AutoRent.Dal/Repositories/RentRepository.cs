using AutoRent.Domain;
using AutoRent.Services.DTOs;
using AutoRent.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Dal.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly AppDataContext context;
        public RentRepository(AppDataContext context)
        {
            this.context = context;
        }

        public void Create(AutoRentOrder entity)
        {
            context.Orders.Add(entity);
        }

        public IEnumerable<AutoRentOrder> GetAll()
        {
            return context.Orders
                .Include(order => order.Renter)
                .Include(order => order.Auto)
                .ToList();
        }

        public AutoRentOrder? GetById(int id)
        {
            return context.Orders
                .Include(order => order.Renter)
                .Include(order => order.Auto)
                .FirstOrDefault(order => order.Id == id);
        }

        public IEnumerable<AutoRentOrder> GetHistoryByRenterId(int id)
        {
            return context.Accounts
                .Include(account => account.Orders)
                .Where(account => account.Id == id)
                .SelectMany(account => account.Orders)
                .Where(order => order.Status == AutoRentOrderStatus.Сompleted)
                .Include(order => order.Renter)
                .Include(order => order.Auto)
                .ToList();
        }

        public IEnumerable<AutoRentOrder> GetOrdersByAutoId(int id)
        {
            return context.Autos
                .Include(auto => auto.Orders)
                .Where(auto => auto.Id == id)
                .SelectMany(auto => auto.Orders)
                .Where(order => order.RentalStartDate >= DateTime.Now)
                .Where(order => order.Status == AutoRentOrderStatus.Waiting)
                .Include(order => order.Renter)
                .Include(order => order.Auto)
                .ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
