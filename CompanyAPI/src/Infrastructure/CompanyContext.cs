using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Company> Company { get; set; }
    }
}