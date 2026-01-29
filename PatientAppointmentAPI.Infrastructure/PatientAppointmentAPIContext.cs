using Microsoft.EntityFrameworkCore;
using PatientAppointmentAPI.Domain.Entities;

namespace PatientAppointmentAPI.Infrastructure
{
    public class PatientAppointmentAPIContext : DbContext
    {
        public PatientAppointmentAPIContext(DbContextOptions<PatientAppointmentAPIContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinician> Clinicians { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //these could be extracted into their own files.
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.NhsNumber)
                      .IsRequired();

                entity.Property(p => p.FirstName)
                      .IsRequired();

                entity.Property(p => p.LastName)
                      .IsRequired();

                // Field-backed navigation
                entity.HasMany<Appointment>("Appointments")
                      .WithOne()
                      .HasForeignKey(a => a.PatientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Status)
                      .HasConversion<int>(); // enum stored as int

                entity.Property(a => a.Department)
                      .HasConversion<int>();

                entity.Property(a => a.DurationMinutes)
                      .IsRequired();
            });

            modelBuilder.Entity<Clinician>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired();
            });
        }
    }
}
