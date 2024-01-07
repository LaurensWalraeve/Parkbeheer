using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Model;


namespace ParkDataLayer
{
    public class ParkbeheerContext : DbContext
    {
        private readonly string _connectionString;

        // Constructor that accepts a connection string
        public ParkbeheerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<ParkEntity> Parken { get; set; }
        public DbSet<HuisEntity> Huizen { get; set; }
        public DbSet<HuurderEntity> Huurders { get; set; }
        public DbSet<HuurcontractEntity> Huurcontracten { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configure your database provider and connection string here
                optionsBuilder.UseSqlServer(_connectionString)
                    .EnableSensitiveDataLogging();  // Add this line to enable sensitive data logging
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration for ParkEntity
            modelBuilder.Entity<ParkEntity>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ParkEntity>()
                .HasMany(p => p.Huizen)
                .WithOne(h => h.Park)
                .HasForeignKey(h => h.ParkId);

            // Configuration for HuisEntity
            modelBuilder.Entity<HuisEntity>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<HuisEntity>()
                .HasMany(h => h.Huurcontracten)
                .WithOne(hc => hc.Huis)
                .HasForeignKey(hc => hc.HuisId);

            // Configuration for HuurderEntity
            modelBuilder.Entity<HuurderEntity>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<HuurderEntity>()
                .OwnsOne(h => h.Contactgegevens, cg =>
                {
                    cg.Property(c => c.Adres).HasMaxLength(100);
                    cg.Property(c => c.Email).HasMaxLength(100);
                    cg.Property(c => c.Tel).HasMaxLength(100);
                });

            modelBuilder.Entity<HuurderEntity>()
                .HasMany(h => h.Huurcontracten)
                .WithOne(hc => hc.Huurder)
                .HasForeignKey(hc => hc.HuurderId);

            // Configuration for HuurcontractEntity
            modelBuilder.Entity<HuurcontractEntity>()
                .HasKey(hc => hc.Id);

            modelBuilder.Entity<HuurcontractEntity>()
                .HasOne(hc => hc.Huurder)
                .WithMany(h => h.Huurcontracten)
                .HasForeignKey(hc => hc.HuurderId);

            modelBuilder.Entity<HuurcontractEntity>()
                .HasOne(hc => hc.Huis)
                .WithMany(h => h.Huurcontracten)
                .HasForeignKey(hc => hc.HuisId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
