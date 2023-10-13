using AutoRent.Services.DTOs;
namespace AutoRent.Services.Interfaces;

public interface IRentService
{
    void Booking(AutoRentOrderBookingDto rentData);
    void Rent(int id);
    decimal Return(int id);
    AutoRentOrderDto GetById(int id);
    IEnumerable<AutoRentOrderDto> GetOrdersByAutoId(int id);
    IEnumerable<AutoRentOrderDto> GetHistoryByRenterId(int id);
}