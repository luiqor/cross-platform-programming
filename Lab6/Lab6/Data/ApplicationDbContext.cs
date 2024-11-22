using Microsoft.EntityFrameworkCore;
using Lab6.Models;

namespace Lab6.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerPaymentMethod> CustomerPaymentMethods { get; set; }
        public DbSet<CustomerOrdersProducts> CustomerOrdersProducts { get; set; }
        public DbSet<CustomerOrdersDelivery> CustomerOrdersDeliveries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfiguration = new DbConfiguration
            {
                DatabaseProvider = _configuration["DatabaseProvider"] ?? throw new ArgumentNullException("DatabaseProvider"),
                ConnectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("DatabaseProvider"),
            };

            switch (dbConfiguration.DatabaseProvider)
            {
                case "SqlServer":
                    optionsBuilder.UseSqlServer(dbConfiguration.ConnectionString);
                    break;
                case "PostgreSQL":
                    optionsBuilder.UseNpgsql(dbConfiguration.ConnectionString);
                    break;
                case "Sqlite":
                    optionsBuilder.UseSqlite(dbConfiguration.ConnectionString);
                    break;
                case "InMemory":
                    optionsBuilder.UseInMemoryDatabase("InMemoryDb");
                    break;
                default:
                    throw new Exception("Unsupported database provider.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca => new { ca.CustomerId, ca.AddressId, ca.DateFrom });

            modelBuilder.Entity<ProductPrice>()
                .HasKey(pp => new { pp.ProductId, pp.DateFrom });

            modelBuilder.Entity<CustomerOrdersProducts>()
                .HasKey(cop => new { cop.OrderId, cop.ProductId });

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierCode)
                .IsRequired(false);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ParentProduct)
                .WithMany()
                .HasForeignKey(p => p.ParentProductId);

            modelBuilder.Entity<ProductPrice>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(pp => pp.ProductId);

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.Customer)
                .WithMany(c => c.CustomerOrders)
                .HasForeignKey(co => co.CustomerId)
                .IsRequired(false);

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.CustomerPaymentMethod)
                .WithMany(cpm => cpm.CustomerOrder)
                .HasForeignKey(co => co.CustomerPaymentMethodId)
                .IsRequired(false);

            modelBuilder.Entity<CustomerOrdersProducts>()
                .HasOne(cop => cop.CustomerOrder)
                .WithMany(co => co.CustomerOrdersProducts)
                .HasForeignKey(cop => cop.OrderId)
                .IsRequired(true);

            modelBuilder.Entity<CustomerOrdersProducts>()
                .HasOne(cop => cop.Product)
                .WithMany(p => p.CustomerOrdersProducts)
                .HasForeignKey(cop => cop.ProductId)
                .IsRequired(true);

            modelBuilder.Entity<CustomerOrdersDelivery>()
                .HasOne(cod => cod.CustomerOrder)
                .WithMany(co => co.CustomerOrdersDeliveries)
                .HasForeignKey(cod => cod.OrderId)
                .IsRequired(true);

            modelBuilder.Entity<CustomerPaymentMethod>()
                .HasOne(cpm => cpm.Customer)
                .WithMany(c => c.CustomerPaymentMethods)
                .HasForeignKey(cpm => cpm.CustomerId)
                .IsRequired(false);

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(cpm => cpm.Customer)
                .WithMany(c => c.CustomerAddresses)
                .HasForeignKey(cpm => cpm.CustomerId);

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(cpm => cpm.Address)
                .WithMany(c => c.CustomerAddresses)
                .HasForeignKey(cpm => cpm.AddressId);
        }
    }
}