using AutoRent.Domain;

namespace AutoRent.Services.DTOs;

public class AutoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Class { get; set; }
    public string Brand { get; set; }
    public string Location { get; set; }
    public decimal CostPerHour { get; set; }

    public static AutoDto FromEntity(Auto entity)
    {
        return new AutoDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Class = entity.Class,
            Brand = entity.Brand,
            Location = entity.Location,
            CostPerHour = entity.CostPerHour,
        };
    }
    
    public Auto ToEntity()
    {
        return new Auto
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description,
            Class = this.Class,
            Brand = this.Brand,
            Location = this.Location,
            CostPerHour = this.CostPerHour,
        };
    }
}