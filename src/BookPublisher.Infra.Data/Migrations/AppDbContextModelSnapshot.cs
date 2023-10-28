﻿// <auto-generated />
using System;
using BookPublisher.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookPublisher.Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BookPublisher.Domain.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FirstName = "Marcos",
                            Gender = "Male",
                            LastName = "Da Silva"
                        },
                        new
                        {
                            Id = 2L,
                            FirstName = "Laurene",
                            Gender = "Female",
                            LastName = "Nobre"
                        });
                });

            modelBuilder.Entity("BookPublisher.Domain.Entities.AuthorPerBook", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorPerBooks");
                });

            modelBuilder.Entity("BookPublisher.Domain.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Edition")
                        .HasColumnType("int");

                    b.Property<DateOnly>("LaunchDate")
                        .HasColumnType("date");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SubTitle")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookPublisher.Domain.Entities.AuthorPerBook", b =>
                {
                    b.HasOne("BookPublisher.Domain.Entities.Author", "Author")
                        .WithMany("AuthorPerBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookPublisher.Domain.Entities.Book", "Book")
                        .WithMany("AuthorPerBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookPublisher.Domain.Entities.Author", b =>
                {
                    b.Navigation("AuthorPerBooks");
                });

            modelBuilder.Entity("BookPublisher.Domain.Entities.Book", b =>
                {
                    b.Navigation("AuthorPerBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
