namespace AutoRent.Services.DTOs;

public class AutoSearchDto
{
    public string? KeyWords { get; set; } = null;
    public string? Location { get; set; } = null;
    public string? Class { get; set; } = null;
    public string? Brand { get; set; } = null;
    public decimal? MinCost { get; set; } = null;
    public decimal? MaxCost { get; set; } = null;
}