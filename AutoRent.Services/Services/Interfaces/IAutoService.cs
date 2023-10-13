using AutoRent.Services.DTOs;
namespace AutoRent.Services.Interfaces;

public interface IAutoService
{
    AutoDto Create(AutoCreateDto createData);
    void Delete(int id);
    void Update(AutoUpdateDto updateData);
    void SetLocation(AutoSetLocationDto setLocationData);
    IEnumerable<AutoDto> GetAllAutos();
    IEnumerable<AutoDto> GetRentalAutos();
    IEnumerable<AutoDto> Search(AutoSearchDto searchData);
    IEnumerable<string> GetAllAutoClasses();
    IEnumerable<string> GetAllAutoBrands();
}