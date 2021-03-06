﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebRegistry.Models;

namespace WebRegistry.Migrations
{
    [DbContext(typeof(ElectronicRegistryDataBaseContext))]
    [Migration("20180607153109_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebRegistry.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("appointmentID");

                    b.Property<DateTime>("DataTime")
                        .HasColumnType("datetime");

                    b.Property<int>("DaysOfWeekId")
                        .HasColumnName("daysOfWeekID");

                    b.Property<int>("DoctorId")
                        .HasColumnName("doctorID");

                    b.Property<int?>("PatientId")
                        .HasColumnName("patientID");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DaysOfWeekId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("WebRegistry.Models.DaysOfWeek", b =>
                {
                    b.Property<int>("DaysOfWeekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("daysOfWeekID");

                    b.Property<string>("DaysOfWeekName")
                        .IsRequired()
                        .HasColumnName("daysOfWeekName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("DaysOfWeekId");

                    b.ToTable("DaysOfWeek");
                });

            modelBuilder.Entity("WebRegistry.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("doctorID");

                    b.Property<int>("HospitalId")
                        .HasColumnName("hospitalID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnName("patronymic")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("SiteId")
                        .HasColumnName("siteID");

                    b.Property<int>("SpecialtyId")
                        .HasColumnName("specialtyID");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("surname")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("DoctorId");

                    b.HasIndex("HospitalId");

                    b.HasIndex("SiteId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("WebRegistry.Models.Hospital", b =>
                {
                    b.Property<int>("HospitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("hospitalID");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnName("hospitalName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("HospitalId");

                    b.ToTable("Hospital");
                });

            modelBuilder.Entity("WebRegistry.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DoctorId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("LikeId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("WebRegistry.Models.NospitalOnStreet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("HospitalId")
                        .HasColumnName("hospitalID");

                    b.Property<int>("StreetId")
                        .HasColumnName("streetID");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.HasIndex("StreetId");

                    b.ToTable("NospitalOnStreet");
                });

            modelBuilder.Entity("WebRegistry.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("patientID");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnName("dateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("MedicalPolicy")
                        .IsRequired()
                        .HasColumnName("medicalPolicy")
                        .HasMaxLength(16)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnName("patronymic")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnName("sex")
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<int>("StreetId")
                        .HasColumnName("streetID");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("surname")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PatientId");

                    b.HasIndex("StreetId");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("WebRegistry.Models.Site", b =>
                {
                    b.Property<int>("SiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("siteID");

                    b.Property<string>("SiteNumber")
                        .IsRequired()
                        .HasColumnName("siteNumber")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.HasKey("SiteId");

                    b.ToTable("Site");
                });

            modelBuilder.Entity("WebRegistry.Models.Specialty", b =>
                {
                    b.Property<int>("SpecialtyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("specialtyID");

                    b.Property<string>("SpecialtyName")
                        .IsRequired()
                        .HasColumnName("specialtyName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("SpecialtyId");

                    b.ToTable("Specialty");
                });

            modelBuilder.Entity("WebRegistry.Models.Street", b =>
                {
                    b.Property<int>("StreetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("streetID");

                    b.Property<string>("Home")
                        .IsRequired()
                        .HasColumnName("home")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<int>("SiteId")
                        .HasColumnName("SiteID");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnName("streetName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("StreetId");

                    b.HasIndex("SiteId");

                    b.ToTable("Street");
                });

            modelBuilder.Entity("WebRegistry.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebRegistry.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebRegistry.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebRegistry.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebRegistry.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebRegistry.Models.Appointment", b =>
                {
                    b.HasOne("WebRegistry.Models.DaysOfWeek", "DaysOfWeek")
                        .WithMany("Appointment")
                        .HasForeignKey("DaysOfWeekId")
                        .HasConstraintName("FK_Appointment_DaysOfWeek");

                    b.HasOne("WebRegistry.Models.Doctor", "Doctor")
                        .WithMany("Appointment")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK_Appointment_Doctor");
                });

            modelBuilder.Entity("WebRegistry.Models.Doctor", b =>
                {
                    b.HasOne("WebRegistry.Models.Hospital", "Hospital")
                        .WithMany("Doctor")
                        .HasForeignKey("HospitalId")
                        .HasConstraintName("FK_Doctor_Hospital");

                    b.HasOne("WebRegistry.Models.Site", "Site")
                        .WithMany("Doctor")
                        .HasForeignKey("SiteId")
                        .HasConstraintName("FK_Doctor_Site");

                    b.HasOne("WebRegistry.Models.Specialty", "Specialty")
                        .WithMany("Doctor")
                        .HasForeignKey("SpecialtyId")
                        .HasConstraintName("FK_Doctor_Specialty");
                });

            modelBuilder.Entity("WebRegistry.Models.Like", b =>
                {
                    b.HasOne("WebRegistry.Models.Doctor", "Doctor")
                        .WithMany("Like")
                        .HasForeignKey("DoctorId")
                        .HasConstraintName("FK_Like_Doctor");
                });

            modelBuilder.Entity("WebRegistry.Models.NospitalOnStreet", b =>
                {
                    b.HasOne("WebRegistry.Models.Hospital", "Hospital")
                        .WithMany("NospitalOnStreet")
                        .HasForeignKey("HospitalId")
                        .HasConstraintName("FK_NospitalOnStreet_Hospital");

                    b.HasOne("WebRegistry.Models.Street", "Street")
                        .WithMany("NospitalOnStreet")
                        .HasForeignKey("StreetId")
                        .HasConstraintName("FK_NospitalOnStreet_Street");
                });

            modelBuilder.Entity("WebRegistry.Models.Patient", b =>
                {
                    b.HasOne("WebRegistry.Models.Street", "Street")
                        .WithMany("Patient")
                        .HasForeignKey("StreetId")
                        .HasConstraintName("FK_Patient_Street");
                });

            modelBuilder.Entity("WebRegistry.Models.Street", b =>
                {
                    b.HasOne("WebRegistry.Models.Site", "Site")
                        .WithMany("Street")
                        .HasForeignKey("SiteId")
                        .HasConstraintName("FK_Street_Site");
                });
#pragma warning restore 612, 618
        }
    }
}
