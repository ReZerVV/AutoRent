using AutoRent.Domain;
using AutoRent.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoRent.Dal.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDataContext context;

        public AccountRepository(AppDataContext context)
        {
            this.context = context;
        }

        public void Create(Account entity)
        {
            context.Add(entity);
        }

        public Account? GetById(int id)
        {
            return context.Accounts
                .Include(account => account.Orders)
                .FirstOrDefault(account => account.Id == id);
        }

        public Account? GetByUsername(string username)
        {
            return context.Accounts
                .Include(account => account.Orders)
                .FirstOrDefault(account => account.Username == username);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
