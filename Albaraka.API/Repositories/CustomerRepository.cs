using Albaraka.API.Data;
using Albaraka.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Albaraka.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ABCBankContext _context;
        public CustomerRepository(ABCBankContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync(); // AsNoTracking performnans için önemli
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task RemoveAsync(int id)
        {
            var removedEntity = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(removedEntity);
            await _context.SaveChangesAsync();
        }

        // değişmemiş olan entity ye değişen alanları setliyorduk
        // önceki değerlerin elimizde olması laızm yoksa hata verir
        public async Task UpdateAsync(Customer customer)
        {
            var unchangedEntity = await _context.Customers.FindAsync(customer.Id);
            _context.Entry(unchangedEntity).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();

        }
    }
}
