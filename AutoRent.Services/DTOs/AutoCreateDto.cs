using AutoRent.Domain;

namespace AutoRent.Services.DTOs;

public class AutoCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Class { get; set; }
    public string Brand { get; set; }
    public decimal CostPerHour { get; set; }
    
    public static AutoCreateDto FromEntity(Auto entity)
    {
        return new AutoCreateDto
        {
            Name = entity.Name,
            Description = entity.Description,
            Class = entity.Class,
            Brand = entity.Brand,
            CostPerHour = entity.CostPerHour,
        };
    }
    
    public Auto ToEntity()
    {
        return new Auto
        {
            Name = this.Name,
            Description = this.Description,
            Class = this.Class,
            Brand = this.Brand,
            Location = string.Empty,
            CostPerHour = this.CostPerHour,
        };
    }
}