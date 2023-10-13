using AutoRent.Domain;

namespace AutoRent.Services.DTOs;

public class AutoRentOrderBookingDto
{
    public int RenterId { get; set; }
    public int AutoId { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }

    public AutoRentOrder ToEntity()
    {
        return new AutoRentOrder
        {
            RenterId = this.RenterId,
            AutoId = this.AutoId,
            RentalStartDate = this.RentalStartDate,
            RentalEndDate = this.RentalEndDate,
        };
    }
}