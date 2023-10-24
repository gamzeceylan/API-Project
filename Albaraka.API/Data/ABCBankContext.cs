using Microsoft.EntityFrameworkCore;

namespace Albaraka.API.Data
{
    public class ABCBankContext : DbContext
    {
        public ABCBankContext(DbContextOptions<ABCBankContext> options) : base(options)
        {

        }
        // property nin özellikleri
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(x => x.CustomerNumber).HasMaxLength(16).IsRequired();
                entity.HasCheckConstraint("CK_CustomerNumber_IsNumeric", "[CustomerNumber] NOT LIKE '%[^0-9]%'");
            });
            modelBuilder.Entity<Customer>().Property(x => x.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(x => x.Surname).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(x => x.PhoneNumber).HasMaxLength(10).IsRequired();
                entity.HasCheckConstraint("CK_PhoneNumber_IsNumeric", "[PhoneNumber] NOT LIKE '%[^0-9]'");
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(x => x.IdentityNumber).HasMaxLength(11).IsRequired().IsUnicode();
                entity.HasCheckConstraint("CK_IdentityNumber_IsNumeric", "[IdentityNumber] NOT LIKE '%[^0-9]'");     
            });


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
