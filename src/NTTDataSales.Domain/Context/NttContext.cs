using Microsoft.EntityFrameworkCore;

namespace NTTDataSales.Domain.Context;

public class NttContext : DbContext
{
    public NttContext(DbContextOptions<NttContext> opts) : base(opts) { }
}
