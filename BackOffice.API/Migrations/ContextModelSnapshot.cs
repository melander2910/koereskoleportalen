﻿// <auto-generated />
using System;
using BackOffice.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackOffice.API.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductionUnitId")
                        .HasColumnType("uuid");

                    b.Property<string>("SubTenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductionUnitId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.Organisation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("AdvertisementProtection")
                        .HasColumnType("boolean");

                    b.Property<string>("CVR")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<bool?>("ClaimedByOwner")
                        .HasColumnType("boolean");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CvrApiModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IndustryCode")
                        .HasColumnType("text");

                    b.Property<string>("IndustryDescription")
                        .HasColumnType("text");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longtitude")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Municipality")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrganisationType")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("text");

                    b.Property<string>("Zipcode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.ProductionUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CVR")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CvrApiModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IndustryCode")
                        .HasColumnType("text");

                    b.Property<string>("IndustryDescription")
                        .HasColumnType("text");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double?>("Longtitude")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Municipality")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrganisationId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("ProductionUnitNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("text");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Zipcode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("ProductionUnits");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.ProductionUnitRemoved", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ProductionUnitId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RemovedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductionUnitId");

                    b.ToTable("ProductionUnitsRemoved");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrganisationUser", b =>
                {
                    b.Property<Guid>("OrganisationsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("OrganisationsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("OrganisationUser");
                });

            modelBuilder.Entity("ProductionUnitUser", b =>
                {
                    b.Property<Guid>("ProductionUnitsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("ProductionUnitsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ProductionUnitUser");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.Course", b =>
                {
                    b.HasOne("BackOffice.API.Models.DatabaseEntities.ProductionUnit", "ProductionUnit")
                        .WithMany("Courses")
                        .HasForeignKey("ProductionUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionUnit");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.ProductionUnit", b =>
                {
                    b.HasOne("BackOffice.API.Models.DatabaseEntities.Organisation", "Organisation")
                        .WithMany("ProductionUnits")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.ProductionUnitRemoved", b =>
                {
                    b.HasOne("BackOffice.API.Models.DatabaseEntities.ProductionUnit", "ProductionUnit")
                        .WithMany("ProductionUnitsRemoved")
                        .HasForeignKey("ProductionUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionUnit");
                });

            modelBuilder.Entity("OrganisationUser", b =>
                {
                    b.HasOne("BackOffice.API.Models.DatabaseEntities.Organisation", null)
                        .WithMany()
                        .HasForeignKey("OrganisationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackOffice.API.Models.DatabaseEntities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductionUnitUser", b =>
                {
                    b.HasOne("BackOffice.API.Models.DatabaseEntities.ProductionUnit", null)
                        .WithMany()
                        .HasForeignKey("ProductionUnitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackOffice.API.Models.DatabaseEntities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.Organisation", b =>
                {
                    b.Navigation("ProductionUnits");
                });

            modelBuilder.Entity("BackOffice.API.Models.DatabaseEntities.ProductionUnit", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("ProductionUnitsRemoved");
                });
#pragma warning restore 612, 618
        }
    }
}
