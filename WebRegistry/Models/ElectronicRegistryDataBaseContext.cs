using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebRegistry.Models;

namespace WebRegistry.Models
{
    public partial class ElectronicRegistryDataBaseContext : IdentityDbContext<User>
    {
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<DaysOfWeek> DaysOfWeek { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Hospital> Hospital { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<NospitalOnStreet> NospitalOnStreet { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<Street> Street { get; set; }
        public ElectronicRegistryDataBaseContext(DbContextOptions<ElectronicRegistryDataBaseContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointmentID");

                entity.Property(e => e.DataTime).HasColumnType("datetime");

                entity.Property(e => e.DaysOfWeekId).HasColumnName("daysOfWeekID");

                entity.Property(e => e.DoctorId).HasColumnName("doctorID");

                entity.Property(e => e.PatientId).HasColumnName("patientID");

                entity.HasOne(d => d.DaysOfWeek)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DaysOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_DaysOfWeek");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Doctor");
            });

            modelBuilder.Entity<DaysOfWeek>(entity =>
            {
                entity.Property(e => e.DaysOfWeekId).HasColumnName("daysOfWeekID");

                entity.Property(e => e.DaysOfWeekName)
                    .IsRequired()
                    .HasColumnName("daysOfWeekName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("doctorID");

                entity.Property(e => e.HospitalId).HasColumnName("hospitalID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasColumnName("patronymic")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteId).HasColumnName("siteID");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialtyID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Hospital");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Site");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Specialty");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.Property(e => e.HospitalId).HasColumnName("hospitalID");

                entity.Property(e => e.HospitalName)
                    .IsRequired()
                    .HasColumnName("hospitalName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Like_Doctor");
            });

            modelBuilder.Entity<NospitalOnStreet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HospitalId).HasColumnName("hospitalID");

                entity.Property(e => e.StreetId).HasColumnName("streetID");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.NospitalOnStreet)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NospitalOnStreet_Hospital");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.NospitalOnStreet)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NospitalOnStreet_Street");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("patientID");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("date");

                entity.Property(e => e.MedicalPolicy)
                    .IsRequired()
                    .HasColumnName("medicalPolicy")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasColumnName("patronymic")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.StreetId).HasColumnName("streetID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Street");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.Property(e => e.SiteId).HasColumnName("siteID");

                entity.Property(e => e.SiteNumber)
                    .IsRequired()
                    .HasColumnName("siteNumber")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.Property(e => e.SpecialtyId).HasColumnName("specialtyID");

                entity.Property(e => e.SpecialtyName)
                    .IsRequired()
                    .HasColumnName("specialtyName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.Property(e => e.StreetId).HasColumnName("streetID");

                entity.Property(e => e.Home)
                    .IsRequired()
                    .HasColumnName("home")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasColumnName("streetName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Street)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Street_Site");
            });
        }
    }
}
