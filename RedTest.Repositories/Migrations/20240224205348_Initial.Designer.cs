﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RedTest.Repositories;

#nullable disable

namespace RedTest.Repositories.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240224205348_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RedTest.Shared.Entities.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("RedTest.Shared.Entities.TopUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<int>("BeneficiaryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiaryId");

                    b.ToTable("TopsUps");
                });

            modelBuilder.Entity("RedTest.Shared.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RedTest.Shared.Entities.Beneficiary", b =>
                {
                    b.HasOne("RedTest.Shared.Entities.User", "User")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RedTest.Shared.Entities.TopUp", b =>
                {
                    b.HasOne("RedTest.Shared.Entities.Beneficiary", "Beneficiary")
                        .WithMany("TopUps")
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiary");
                });

            modelBuilder.Entity("RedTest.Shared.Entities.Beneficiary", b =>
                {
                    b.Navigation("TopUps");
                });

            modelBuilder.Entity("RedTest.Shared.Entities.User", b =>
                {
                    b.Navigation("Beneficiaries");
                });
#pragma warning restore 612, 618
        }
    }
}
