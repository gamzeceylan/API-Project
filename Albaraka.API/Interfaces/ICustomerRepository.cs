using Albaraka.API.Data;

namespace Albaraka.API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(int id);
    }
}
