using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace TestForHH.ORM
{
    public class HospitalContext : DbContext
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hhtest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<Area> Areas { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to many
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Area)
                .WithMany(u => u.Patients)
                .HasForeignKey(p => p.AreaId);

            modelBuilder.Entity<Doctor>()
                .HasOne(v => v.Cabinet)
                .WithMany(k => k.Doctors)
                .HasForeignKey(v => v.CabinetId);

            modelBuilder.Entity<Doctor>()
                .HasOne(v => v.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(v => v.SpecializationId);

            modelBuilder.Entity<Doctor>()
                .HasOne(v => v.Area)
                .WithMany(u => u.Doctors)
                .HasForeignKey(v => v.AreaId);
        }
    }
}
