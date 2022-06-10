﻿// <auto-generated />
using System;
using MediatrExample.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediatrExample.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220610154215_pw alani")]
    partial class pwalani
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediatrExample.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CREATED_TIME");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<string>("Gsm")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("GSM");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("LAST_NAME");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("MAIL");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("PW_HASH");

                    b.Property<DateTime?>("UpdatedTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("UPDATED_TIME");

                    b.HasKey("Id");

                    b.ToTable("USERS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTime(2022, 6, 10, 18, 42, 15, 172, DateTimeKind.Local).AddTicks(478),
                            FirstName = "Hasan",
                            Gsm = "5555555555",
                            LastName = "Erdal",
                            Mail = "test@test.com",
                            PasswordHash = "",
                            UpdatedTime = new DateTime(2022, 6, 10, 18, 42, 15, 172, DateTimeKind.Local).AddTicks(480)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
