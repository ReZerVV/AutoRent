namespace AutoRent.Services.DTOs;

public class AutoUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Class { get; set; }
    public string Brand { get; set; }
    public decimal CostPerHour { get; set; }
}