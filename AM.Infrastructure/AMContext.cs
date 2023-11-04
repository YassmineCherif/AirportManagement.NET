using AM.ApplicationCore.Domain;
using AM.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure
{
    public class AMContext:DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
          Initial Catalog=AirportManagementDB;
          Integrated Security=true;
          MultipleActiveResultSets=true");
            //Activer LazyLoading
            optionsBuilder.UseLazyLoadingProxies();
          base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new PlaneConfiguration());
            modelBuilder.Entity<Plane>().HasKey(p => p.PlaneId);
            modelBuilder.Entity<Plane>().ToTable("MyPlanes");
            modelBuilder.Entity<Plane>().Property(p => p.Capacity)
                .HasColumnName("PlaneCapacity");
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            //Configure Owned Type
            modelBuilder.Entity<Passenger>().OwnsOne(p => p.FullName);
            //Configurer l'heritage table Par Hierarchie (TPH)
            //modelBuilder.Entity<Passenger>().HasDiscriminator<int>("IsTraveller")
            //                                .HasValue<Passenger>(0)
            //                                .HasValue<Traveller>(1)
            //                                .HasValue<Staff>(2);
            //configurer l'approche Table Par Type(TPT)
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Traveller>().ToTable("Travellers");
            //Configurer la clé primaire de la table porteuse de données
            modelBuilder.Entity<Ticket>()
                .HasKey(t =>new { t.PassengerFK, t.FlightFK});
            base.OnModelCreating(modelBuilder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
            base.ConfigureConventions(configurationBuilder);
        }

    }
}
