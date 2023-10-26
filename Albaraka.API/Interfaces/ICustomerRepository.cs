using Albaraka.API.Data;
using System.Linq.Expressions;

namespace Albaraka.API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(int id);
        Task<Customer> GetByFilter(Expression<Func<Customer, bool>> filter, bool asNoTracking = false);

    }
}
