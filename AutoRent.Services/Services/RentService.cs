using AutoRent.Data;
using AutoRent.Domain;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace  AutoRent.Services;

public class RentService : IRentService
{
    private readonly AppDataContext _context;

    public RentService(AppDataContext context)
    {
        _context = context;
    }

    public void Booking(AutoRentOrderBookingDto rentData)
    {
        if (!_context.Accounts.Any(account => account.Id == rentData.RenterId))
        {
            throw new ValidationException("Renter id not found");
        }
        if (!_context.Autos.Any(auto => auto.Id == rentData.AutoId))
        {
            throw new ValidationException("Auto id not found");
        }
        if (rentData.RentalStartDate.AddMinutes(1) < DateTime.Now || 
            (rentData.RentalEndDate - rentData.RentalStartDate).Hours < 1)
        {
            throw new ValidationException("Invalid start or end rental date");
        }
        var autoRentOrder = rentData.ToEntity();
        autoRentOrder.Status = AutoRentOrderStatus.Waiting;
        _context.Orders.Add(autoRentOrder);
        _context.SaveChanges();
    }

    public void Rent(int id)
    {
        var autoRentOrder = _context.Orders.Find(id);
        if (autoRentOrder == null)
        {
            throw new ValidationException("Rent id not found");
        }
        if (autoRentOrder.Status == AutoRentOrderStatus.Сompleted)
        {
            throw new ValidationException("The order completed");
        }
        autoRentOrder.Status = AutoRentOrderStatus.Progress;
        _context.SaveChanges();
    }

    public decimal Return(int id)
    {
        var autoRentOrder = _context.Orders
            .Include(order => order.Auto)
            .Where(order => order.Id == id)
            .SingleOrDefault();
        if (autoRentOrder == null)
        {
            throw new ValidationException("Rent id not found");
        }
        if (autoRentOrder.Status != AutoRentOrderStatus.Progress)
        {
            throw new ValidationException("The request is pending or has already been completed");
        }
        autoRentOrder.Status = AutoRentOrderStatus.Сompleted;
        autoRentOrder.Cost = (autoRentOrder.RentalEndDate - autoRentOrder.RentalStartDate).Hours *
                             autoRentOrder.Auto.CostPerHour;
        _context.SaveChanges();
        return autoRentOrder.Cost;
    }

    public IEnumerable<AutoRentOrderDto> GetOrdersByAutoId(int id)
    {
        if (!_context.Accounts.Any(account => account.Id == id))
        {
            throw new ValidationException("Renter id not found");
        }
        return _context.Autos
            .Include(auto => auto.Orders)
            .Where(auto => auto.Id == id)
            .SelectMany(auto => auto.Orders)
            .Where(order => order.RentalStartDate >= DateTime.Now)
            .Where(order => order.Status == AutoRentOrderStatus.Waiting)
            .Include(order => order.Renter)
            .Include(order => order.Auto)
            .Select(order => AutoRentOrderDto.FromEntity(order))
            .ToList();
    }

    public IEnumerable<AutoRentOrderDto> GetHistoryByRenterId(int id)
    {
        if (!_context.Accounts.Any(auto => auto.Id == id))
        {
            throw new ValidationException("Auto id not found");
        }
        return _context.Accounts
            .Include(account => account.Orders)
            .Where(account => account.Id == id)
            .SelectMany(account => account.Orders)
            .Where(order => order.Status == AutoRentOrderStatus.Сompleted)
            .Include(order => order.Renter)
            .Include(order => order.Auto)
            .Select(order => AutoRentOrderDto.FromEntity(order))
            .ToList();
    }

    public AutoRentOrderDto? GetById(int id)
    {
        return _context.Orders
            .Include(order => order.Renter)
            .Include(order => order.Auto)
            .Where(order => order.Id == id)
            .Select(order => AutoRentOrderDto.FromEntity(order))
            .SingleOrDefault();
    }
}