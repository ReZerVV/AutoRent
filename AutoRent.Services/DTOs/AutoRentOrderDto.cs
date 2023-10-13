using AutoRent.Domain;
namespace AutoRent.Services.DTOs;

public class AutoRentOrderDto
{
    public int Id { get; set; }
    public AccountDto Renter { get; set; }
    public AutoDto Auto { get; set; }
    public AutoRentOrderStatus Status { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public decimal Cost { get; set; }

    public static AutoRentOrderDto FromEntity(AutoRentOrder entity)
    {
        return new AutoRentOrderDto
        {
            Id = entity.Id,
            Renter = AccountDto.FromEntity(entity.Renter),
            Auto = AutoDto.FromEntity(entity.Auto),
            Status = entity.Status,
            RentalStartDate = entity.RentalStartDate,
            RentalEndDate = entity.RentalEndDate,
            Cost = entity.Cost,
        };
    }
}