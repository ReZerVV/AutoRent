using AutoRent.Domain;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using AutoRent.Services.Repositories;
using System.Text.RegularExpressions;

namespace AutoRent.Services;

public class AutoService : IAutoService
{
    private readonly IAutoRepository autoRepository;

    public AutoService(IAutoRepository autoRepository)
    {
        this.autoRepository = autoRepository;
    }

    public AutoDto Create(AutoCreateDto createData)
    {
        if (string.IsNullOrEmpty(createData.Name) ||
            string.IsNullOrEmpty(createData.Description) ||
            string.IsNullOrEmpty(createData.Class) ||
            string.IsNullOrEmpty(createData.Brand))
        {
            throw new ValidationException("One field, some empty, fill in and try again.");
        }
        var auto = createData.ToEntity();
        autoRepository.Create(auto);
        autoRepository.SaveChanges();
        return AutoDto.FromEntity(auto);
    }
    
    public void Delete(int id)
    {
        var auto = autoRepository.GetById(id);
        if (auto == null)
        {
            throw new ValidationException("Auto id not found");
        }
        autoRepository.Remove(auto);
        autoRepository.SaveChanges();
    }

    public void Update(AutoUpdateDto updateData)
    {
        var auto = autoRepository.GetById(updateData.Id);
        if (auto == null)
        {
            throw new ValidationException("Auto id not found");
        }
        auto.Name = updateData.Name;
        auto.Description = updateData.Description;
        auto.Class = updateData.Class;
        auto.Brand = updateData.Brand;
        auto.CostPerHour = updateData.CostPerHour;
        autoRepository.SaveChanges();
    }

    public void SetLocation(AutoSetLocationDto setLocationData)
    {
        var auto = autoRepository.GetById(setLocationData.Id);
        if (auto == null)
        {
            throw new ValidationException("Auto id not found");
        }
        if (string.IsNullOrEmpty(setLocationData.Location) ||
            !Regex.IsMatch(setLocationData.Location, "^[ A-Z]+,+[ a-zA-Z]+,+[ a-zA-Z]+,+[ a-zA-Z]+,+[ 0-9]*$")) 
        {
            throw new ValidationException("Location invalid format. Please re-enter location. (Ex. UA,Zaporizhzhia region,Zaporizhzhia,Schevchenko,1)");
        }
        auto.Location = setLocationData.Location;
        autoRepository.SaveChanges();
    }

    public IEnumerable<AutoDto> GetRentalAutos()
    {
        return autoRepository.GetAll()
            .Where(auto => auto.Orders.Any(order => order.Status == AutoRentOrderStatus.Progress))
            .Select(auto => AutoDto.FromEntity(auto))
            .ToList();
    }

    public IEnumerable<AutoDto> Search(AutoSearchDto searchData)
    {
        var autos = autoRepository.GetAll()
            .Where(auto => auto.Location != string.Empty)
            .ToList();
        if (!string.IsNullOrEmpty(searchData.KeyWords))
        {
            autos = autos.Where(auto => searchData.KeyWords.Any(keyWord => auto.Name.Contains(keyWord) || 
                auto.Description.Contains(keyWord)))
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchData.Location))
        {
            autos = autos.Where(auto => auto.Location == searchData.Location)
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchData.Class))
        {
            autos = autos.Where(auto => auto.Class == searchData.Class)
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchData.Brand))
        {
            autos = autos.Where(auto => auto.Brand == searchData.Brand)
                .ToList();
        }
        if (searchData.MinCost != 0 &&
            searchData.MinCost != null &&
            searchData.MaxCost != 0 &&
            searchData.MaxCost != null)
        {
            autos = autos.Where(auto => searchData.MinCost <= auto.CostPerHour && auto.CostPerHour < searchData.MaxCost)
                .ToList();
        }
        return autos.Select(auto => AutoDto.FromEntity(auto))
            .ToList();
    }

    public IEnumerable<string> GetAllAutoClasses()
    {
        return autoRepository.GetAllClasses();
    }

    public IEnumerable<string> GetAllAutoBrands()
    {
        return autoRepository.GetAllBrands();
    }

    public IEnumerable<AutoDto> GetAllAutos()
    {
        return autoRepository.GetAll()
            .Select(auto => AutoDto.FromEntity(auto))
            .ToList();
    }
}

