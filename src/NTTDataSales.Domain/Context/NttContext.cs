using Microsoft.EntityFrameworkCore;
using NTTDataSales.Domain.Entities;

namespace NTTDataSales.Domain.Context;

public class NttContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public NttContext(DbContextOptions<NttContext> opts) : base(opts) { }
}
