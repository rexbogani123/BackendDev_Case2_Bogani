using BackendDev_Case2_Bogani.Model;
using Microsoft.EntityFrameworkCore;

namespace BackendDev_Case2_Bogani.Data
{
    public class FinancialDbContext : DbContext
    {
    public FinancialDbContext(DbContextOptions<FinancialDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          base.OnModelCreating(modelBuilder);
        
    }

    }
}