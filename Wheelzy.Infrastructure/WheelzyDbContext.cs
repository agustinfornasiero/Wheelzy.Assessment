
using Microsoft.EntityFrameworkCore;
using Wheelzy.Domain.Entities;

namespace Wheelzy.Infrastructure
{
    public class WheelzyDbContext : DbContext
    {
        public WheelzyDbContext(DbContextOptions<WheelzyDbContext> options) : base(options) { }

        // DbSets
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
        public DbSet<CaseStatus> CaseStatuses => Set<CaseStatus>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<Order> Orders => Set<Order>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Índices únicos
            modelBuilder.Entity<Model>()
                .HasIndex(x => new { x.MakeId, x.Name })
                .IsUnique();

            modelBuilder.Entity<SubModel>()
                .HasIndex(x => new { x.ModelId, x.Name })
                .IsUnique();

            modelBuilder.Entity<CaseQuote>()
                .HasIndex(x => x.CaseId)
                .IsUnique()
                .HasFilter("[IsCurrent] = 1");

            modelBuilder.Entity<CaseStatus>()
                .HasIndex(x => x.CaseId)
                .IsUnique()
                .HasFilter("[IsCurrent] = 1");

            // Clave primaria ZipCode
            modelBuilder.Entity<ZipCode>()
                .HasKey(z => z.Zip);

            // Mapear tablas explícitamente para evitar pluralización incorrecta
            modelBuilder.Entity<Make>().ToTable("Makes");
            modelBuilder.Entity<Model>().ToTable("Models");
            modelBuilder.Entity<SubModel>().ToTable("Submodels");
            modelBuilder.Entity<ZipCode>().ToTable("ZipCodes");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Buyer>().ToTable("Buyers");
            modelBuilder.Entity<BuyerZipQuote>().ToTable("BuyerZipQuotes");
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Case>().ToTable("Cases");
            modelBuilder.Entity<CaseQuote>().ToTable("CaseQuotes");
            modelBuilder.Entity<CaseStatus>().ToTable("CaseStatus");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Order>().ToTable("Orders");

            // Configurar relaciones

            // Relaciones de Model con Make
            modelBuilder.Entity<Model>()
                .HasOne(m => m.Make)
                .WithMany()
                .HasForeignKey(m => m.MakeId);

            // Relaciones de SubModel con Model
            modelBuilder.Entity<SubModel>()
                .HasOne(sm => sm.Model)
                .WithMany()
                .HasForeignKey(sm => sm.ModelId);

            // Relaciones de Car con Make
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Make)
                .WithMany()
                .HasForeignKey(c => c.MakeId);

            // Relaciones de Car con SubModel
            modelBuilder.Entity<Car>()
                .HasOne(c => c.SubModel)
                .WithMany()
                .HasForeignKey(c => c.SubModelId);

            // Relaciones de BuyerZipQuote con Buyer
            modelBuilder.Entity<BuyerZipQuote>()
                .HasOne(bzq => bzq.Buyer)
                .WithMany()
                .HasForeignKey(bzq => bzq.BuyerId);

            // Relaciones de BuyerZipQuote con ZipCode
            modelBuilder.Entity<BuyerZipQuote>()
                .HasOne(bzq => bzq.ZipCode)
                .WithMany()
                .HasForeignKey(bzq => bzq.Zip)
                .HasPrincipalKey(z => z.Zip);

            // Relaciones de Case con Customer
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId);

            // Relaciones de Case con Car
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Car)
                .WithMany()
                .HasForeignKey(c => c.CarId);

            // Relaciones de CaseQuote con Case
            modelBuilder.Entity<CaseQuote>()
                .HasOne(cq => cq.Case)
                .WithMany(c => c.CaseQuotes)
                .HasForeignKey(cq => cq.CaseId);

            // Relaciones de CaseQuote con BuyerZipQuote
            modelBuilder.Entity<CaseQuote>()
                .HasOne(cq => cq.BuyerZipQuote)
                .WithMany()
                .HasForeignKey(cq => cq.BuyerZipQuoteId);

            // Relaciones de CaseStatus con Case
            modelBuilder.Entity<CaseStatus>()
                .HasOne(cs => cs.Case)
                .WithMany(c => c.CaseStatuses)
                .HasForeignKey(cs => cs.CaseId);

            // Relaciones de CaseStatus con Status
            modelBuilder.Entity<CaseStatus>()
                .HasOne(cs => cs.Status)
                .WithMany()
                .HasForeignKey(cs => cs.StatusId);

            // Relaciones de Order con Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

            // Relaciones de Order con Status
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId);
        }
    }
}

