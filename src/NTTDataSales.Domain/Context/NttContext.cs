using Microsoft.EntityFrameworkCore;
using NTTDataSales.Domain.Entities;

namespace NTTDataSales.Domain.Context;

public class NttContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public NttContext(DbContextOptions<NttContext> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>().HasMany(x => x.SALEITEMS);
    }
}
