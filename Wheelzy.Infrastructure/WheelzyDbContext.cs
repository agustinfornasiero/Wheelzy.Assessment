

using Microsoft.EntityFrameworkCore;
using Wheelzy.Domain.Entities;

namespace Wheelzy.Infrastructure
{
    public class WheelzyDbContext : DbContext
    {
        public WheelzyDbContext(DbContextOptions<WheelzyDbContext> options) : base(options) { }

        public DbSet<Make> Makes => Set<Make>();
        public DbSet<Model> Models => Set<Model>();
        public DbSet<SubModel> SubModels => Set<SubModel>();
        public DbSet<ZipCode> ZipCodes => Set<ZipCode>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Buyer> Buyers => Set<Buyer>();
        public DbSet<BuyerZipQuote> BuyerZipQuotes => Set<BuyerZipQuote>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Case> Cases => Set<Case>();
        public DbSet<CaseQuote> CaseQuotes => Set<CaseQuote>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<CaseStatus> CaseStatuses => Set<CaseStatus>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>().HasIndex(x => new { x.MakeId, x.Name }).IsUnique();
            modelBuilder.Entity<SubModel>().HasIndex(x => new { x.ModelId, x.Name }).IsUnique();
            modelBuilder.Entity<CaseQuote>().HasIndex(x => x.CaseId).IsUnique().HasFilter("[IsCurrent] = 1");
            modelBuilder.Entity<CaseStatus>().HasIndex(x => x.CaseId).IsUnique().HasFilter("[IsCurrent] = 1");
            modelBuilder.Entity<ZipCode>().HasKey(z => z.Zip);
        }
    }


}
