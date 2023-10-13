using AutoRent.Domain;

namespace AutoRent.Services.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account entity);
        Account? GetById(int id);
        Account? GetByUsername(string username);
        void SaveChanges();
    }
}
