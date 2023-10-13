namespace AutoRent.Domain;

public class AutoRentOrder
{
    public int Id { get; set; }
    public int RenterId { get; set; }
    public Account Renter { get; set; }
    public int AutoId { get; set; }
    public Auto Auto { get; set; }
    public AutoRentOrderStatus Status { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public decimal Cost { get; set; }
}