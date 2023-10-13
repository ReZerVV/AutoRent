using AutoRent.Domain;

namespace AutoRent.Services.Repositories
{
    public interface IRentRepository
    {
        void Create(AutoRentOrder entity);
        AutoRentOrder? GetById(int id);
        IEnumerable<AutoRentOrder> GetAll();
        IEnumerable<AutoRentOrder> GetOrdersByAutoId(int id);
        IEnumerable<AutoRentOrder> GetHistoryByRenterId(int id);
        void SaveChanges();
    }
}
