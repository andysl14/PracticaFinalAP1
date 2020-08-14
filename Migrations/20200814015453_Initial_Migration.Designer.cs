﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracticaFinalAP1.DAL;

namespace PracticaFinalAP1.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200814015453_Initial_Migration")]
    partial class Initial_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("PracticaFinalAP1.Entidades.Amigos", b =>
                {
                    b.Property<int>("AmigoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .HasColumnType("TEXT");

                    b.Property<string>("Celular")
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.HasKey("AmigoId");

                    b.ToTable("Amigos");
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.EntradaJuegos", b =>
                {
                    b.Property<int>("EntradaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<int>("JuegoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EntradaId");

                    b.HasIndex("JuegoId");

                    b.ToTable("EntradasJuegos");
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.Juegos", b =>
                {
                    b.Property<int>("JuegoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<int>("Existencia")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("TEXT");

                    b.Property<float>("Precio")
                        .HasColumnType("REAL");

                    b.HasKey("JuegoId");

                    b.ToTable("Juegos");
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.Prestamos", b =>
                {
                    b.Property<int>("PrestamoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmigoId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CantidadJuegos")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacion")
                        .HasColumnType("TEXT");

                    b.HasKey("PrestamoId");

                    b.HasIndex("AmigoId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.PrestamosDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JuegoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrestamoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JuegoId");

                    b.HasIndex("PrestamoId");

                    b.ToTable("PrestamosDetalle");
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.EntradaJuegos", b =>
                {
                    b.HasOne("PracticaFinalAP1.Entidades.Juegos", "juegos")
                        .WithMany()
                        .HasForeignKey("JuegoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.Prestamos", b =>
                {
                    b.HasOne("PracticaFinalAP1.Entidades.Amigos", "amigos")
                        .WithMany()
                        .HasForeignKey("AmigoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PracticaFinalAP1.Entidades.PrestamosDetalle", b =>
                {
                    b.HasOne("PracticaFinalAP1.Entidades.Juegos", "juegos")
                        .WithMany()
                        .HasForeignKey("JuegoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticaFinalAP1.Entidades.Prestamos", null)
                        .WithMany("Detalle")
                        .HasForeignKey("PrestamoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
