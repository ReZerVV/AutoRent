using AutoRent.Data;
using AutoRent.Domain;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AutoRent.Services;

public class AutoService : IAutoService
{
    private readonly AppDataContext _context;

    public AutoService(AppDataContext context)
    {
        _context = context;
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
        _context.Autos.Add(auto);
        _context.SaveChanges();
        return AutoDto.FromEntity(auto);
    }
    
    public void Delete(int id)
    {
        var auto = _context.Autos.FirstOrDefault(auto => auto.Id == id);
        if (auto == null)
        {
            throw new ValidationException("Auto id not found");
        }
        _context.Autos.Remove(auto);
        _context.SaveChanges();
    }

    public void Update(AutoUpdateDto updateData)
    {
        var auto = _context.Autos.FirstOrDefault(auto => auto.Id == updateData.Id);
        if (auto == null)
        {
            throw new ValidationException("Auto id not found");
        }
        auto.Name = updateData.Name;
        auto.Description = updateData.Description;
        auto.Class = updateData.Class;
        auto.Brand = updateData.Brand;
        auto.CostPerHour = updateData.CostPerHour;
        _context.SaveChanges();
    }

    public void SetLocation(AutoSetLocationDto setLocationData)
    {
        var auto = _context.Autos.FirstOrDefault(auto => auto.Id == setLocationData.Id);
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
        _context.SaveChanges();
    }

    public IEnumerable<AutoDto> GetRentalAutos()
    {
        return _context.Autos
            .Include(auto => auto.Orders)
            .Where(auto => auto.Orders.Any(order => order.Status == AutoRentOrderStatus.Progress))
            .Select(auto => AutoDto.FromEntity(auto))
            .ToList();
    }

    public IEnumerable<AutoDto> Search(AutoSearchDto searchData)
    {
        var autos = _context.Autos
            .Where(auto => auto.Location != string.Empty)
            .ToList();
        if (!string.IsNullOrEmpty(searchData.KeyWords))
        {
            autos = autos.Where(auto => searchData.KeyWords.Any(keyWord => auto.Name.Contains(keyWord) || 
                auto.Name.Contains(keyWord)))
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
            searchData.MinCost != 0)
        {
            autos = autos.Where(auto => searchData.MinCost <= auto.CostPerHour && auto.CostPerHour < searchData.MaxCost)
                .ToList();
        }
        return autos.Select(auto => AutoDto.FromEntity(auto))
            .ToList();
    }

    public IEnumerable<string> GetAllAutoClasses()
    {
        return _context.Autos
            .Select(auto => auto.Class)
            .Distinct()
            .ToList();
    }

    public IEnumerable<string> GetAllAutoBrands()
    {
        return _context.Autos
            .Select(auto => auto.Brand)
            .Distinct()
            .ToList();
    }

    public IEnumerable<AutoDto> GetAllAutos()
    {
        return _context.Autos
            .Select(auto => AutoDto.FromEntity(auto))
            .ToList();
    }
}

