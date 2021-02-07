using Microsoft.EntityFrameworkCore;
using Funda.Data.Entities;

namespace Funda.Data
{
    public class FundaDbContext : DbContext
    {
        public FundaDbContext()
        {
        }

        public FundaDbContext(DbContextOptions<FundaDbContext> options) : base(options)
        {

        }
        public DbSet<Object> Objects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
