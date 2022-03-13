﻿// <auto-generated />
using System;
using Koeheya.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Koeheya.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Koeheya.Data.ApplicationUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("UserId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("UserName");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Koeheya.Data.Heya", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("Height");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<string>("Owner")
                        .HasColumnType("text")
                        .HasColumnName("Owner");

                    b.Property<int>("Width")
                        .HasColumnType("integer")
                        .HasColumnName("Width");

                    b.Property<int>("X")
                        .HasColumnType("integer")
                        .HasColumnName("X");

                    b.Property<int>("Y")
                        .HasColumnType("integer")
                        .HasColumnName("Y");

                    b.HasKey("Id");

                    b.ToTable("Heya");
                });
#pragma warning restore 612, 618
        }
    }
}
