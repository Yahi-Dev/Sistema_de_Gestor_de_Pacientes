using Microsoft.EntityFrameworkCore;
using SGDP.Core.Domain.Common;
using SGDP.Core.Domain.Entities;

namespace SGDP.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> optiones) : base(optiones) { }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<LabTests> TestLabs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreateBy = "Admin";
                        entry.Entity.LastModifiedBy = "Admin";
                    break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Admin";
                        entry.Property("CreateBy").IsModified = false;
                    break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API
            #region tables
            modelBuilder.Entity<Medico>().ToTable("Medicos");
            modelBuilder.Entity<LabTests>().ToTable("LabTests");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Result>().ToTable("Result");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Medico>().HasKey(medico => medico.Id); //Lambda
            modelBuilder.Entity<LabTests>().HasKey(labtests => labtests.Id);
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Patient>().HasKey(patient => patient.Id);
            modelBuilder.Entity<Result>().HasKey(result => result.Id);
            modelBuilder.Entity<Appointment>().HasKey(appointment => appointment.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Medico1)
            .WithMany(m => m.Appointments)
            .HasForeignKey(a => a.medicoID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.LabTests1)
                .WithMany()
                .HasForeignKey(a => a.LabTestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Patient1)
                .WithMany(p => p.results)
                .HasForeignKey(r => r.patientId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region "property configuration"

            #region user
            modelBuilder.Entity<User>()
                .Property(user => user.UserName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.LastName)
                .IsRequired();
            #endregion

            #region medico
            modelBuilder.Entity<Medico>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Medico>()
                .Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(100);


            modelBuilder.Entity<Medico>()
                .Property(p => p.Specialization)
                .IsRequired()
                .HasMaxLength(200);


            modelBuilder.Entity<Medico>()
                .Property(p => p.IdCedula)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Medico>()
                .Property(medico => medico.Email)
                .IsRequired();

            modelBuilder.Entity<Medico>()
                .Property(medico => medico.LastName)
                .IsRequired();
            #endregion

            #region Pacientes

            modelBuilder.Entity<Patient>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Sex)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Cedula)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.FechaDeNacimiento)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Question1)
                .HasColumnName("¿Es Alergico?")
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Question2)
                .HasColumnName("¿Es Fumador?")
                .IsRequired()
                .HasMaxLength(200);
            #endregion

            #region Pruebas de laboratio
            modelBuilder.Entity<LabTests>()
                .Property(c => c.TestName)
                .IsRequired()
                .HasMaxLength(200);
            #endregion

            #region Result
            modelBuilder.Entity<Result>()
                .Property(c => c.NamePatient)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Result>()
                .Property(c => c.LastNamePatient)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Result>()
                .Property(c => c.cedulapatient)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Result>()
                .Property(c => c.NameLabTest)
                .IsRequired()
                .HasMaxLength(200);
            #endregion

            #region Appointment
            modelBuilder.Entity<Appointment>()
                .Property(c => c.Patiente)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Appointment>()
                .Property(c => c.Doctor)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Appointment>()
                .Property(c => c.why)
                .IsRequired()
                .HasMaxLength(200);
            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
