﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkyAPI.Datos;

namespace ParkyAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParkyAPI.Modelos.ParqueNacional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComunidadAutonoma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Establecido")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("parquesNacionales");
                });

            modelBuilder.Entity("ParkyAPI.Modelos.SenderoDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<int>("Dificultad")
                        .HasColumnType("int");

                    b.Property<double>("Distancia")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParqueNacionalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParqueNacionalId");

                    b.ToTable("senderos");
                });

            modelBuilder.Entity("ParkyAPI.Modelos.SenderoDto", b =>
                {
                    b.HasOne("ParkyAPI.Modelos.ParqueNacional", "ParqueNacional")
                        .WithMany()
                        .HasForeignKey("ParqueNacionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParqueNacional");
                });
#pragma warning restore 612, 618
        }
    }
}
