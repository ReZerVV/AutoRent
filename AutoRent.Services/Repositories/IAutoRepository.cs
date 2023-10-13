using AutoRent.Domain;

namespace AutoRent.Services.Repositories
{
    public interface IAutoRepository
    {
        void Create(Auto entity);
        void Remove(Auto entity);
        Auto? GetById(int id);
        IEnumerable<Auto> GetAll();
        IEnumerable<string> GetAllBrands();
        IEnumerable<string> GetAllClasses();
        void SaveChanges();
    }
}
