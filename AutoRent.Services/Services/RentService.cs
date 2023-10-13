using AutoRent.Domain;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using AutoRent.Services.Repositories;

namespace  AutoRent.Services;

public class RentService : IRentService
{
    private readonly IRentRepository rentRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IAutoRepository autoRepository;

    public RentService(
        IRentRepository rentRepository,
        IAccountRepository accountRepository,
        IAutoRepository autoRepository)
    {
        this.rentRepository = rentRepository;
        this.accountRepository = accountRepository;
        this.autoRepository = autoRepository;
    }

    public void Booking(AutoRentOrderBookingDto rentData)
    {
        if (accountRepository.GetById(rentData.RenterId) == null)
        {
            throw new ValidationException("Renter id not found");
        }
        if (autoRepository.GetById(rentData.AutoId) == null)
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
        rentRepository.Create(autoRentOrder);
        rentRepository.SaveChanges();
    }

    public void Rent(int id)
    {
        var autoRentOrder = rentRepository.GetById(id);
        if (autoRentOrder == null)
        {
            throw new ValidationException("Rent id not found");
        }
        if (autoRentOrder.Status == AutoRentOrderStatus.Сompleted)
        {
            throw new ValidationException("The order completed");
        }
        autoRentOrder.Status = AutoRentOrderStatus.Progress;
        rentRepository.SaveChanges();
    }

    public decimal Return(int id)
    {
        var autoRentOrder = rentRepository.GetById(id);
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
        rentRepository.SaveChanges();
        return autoRentOrder.Cost;
    }

    public IEnumerable<AutoRentOrderDto> GetOrdersByAutoId(int id)
    {
        if (autoRepository.GetById(id) == null)
        {
            throw new ValidationException("Auto id not found");
        }
        return rentRepository.GetOrdersByAutoId(id)
            .Select(order => AutoRentOrderDto.FromEntity(order))
            .ToList();
    }

    public IEnumerable<AutoRentOrderDto> GetHistoryByRenterId(int id)
    {
        if (accountRepository.GetById(id) == null)
        {
            throw new ValidationException("Renter id not found");
        }
        return rentRepository.GetHistoryByRenterId(id)
            .Select(order => AutoRentOrderDto.FromEntity(order))
            .ToList();
    }

    public AutoRentOrderDto? GetById(int id)
    {
        var entity = rentRepository.GetById(id);
        if (entity == null)
            return null;
        return AutoRentOrderDto.FromEntity(entity);
    }
}