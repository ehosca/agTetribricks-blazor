using Microsoft.EntityFrameworkCore;

namespace TetriBricks.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TbScore> Scores => Set<TbScore>();
}
