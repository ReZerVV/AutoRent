using AutoRent.Domain;
using AutoRent.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Dal.Repositories
{
    public class AutoRepository : IAutoRepository
    {
        private readonly AppDataContext context;
        public AutoRepository(AppDataContext context)
        {
            this.context = context;
        }

        public void Create(Auto entity)
        {
            context.Autos.Add(entity);
        }

        public IEnumerable<Auto> GetAll()
        {
            return context.Autos
                .Include(auto => auto.Orders)
                .ToList();
        }

        public IEnumerable<string> GetAllBrands()
        {
            return context.Autos
            .Select(auto => auto.Brand)
            .Distinct()
            .ToList();
        }

        public IEnumerable<string> GetAllClasses()
        {
            return context.Autos
            .Select(auto => auto.Class)
            .Distinct()
            .ToList();
        }

        public Auto? GetById(int id)
        {
            return context.Autos
                .Include(auto => auto.Orders)
                .FirstOrDefault(auto => auto.Id == id);
        }

        public void Remove(Auto entity)
        {
            context.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
