﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _7Assist.Data;

#nullable disable

namespace _7Assist.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241117135231_AddMoreData")]
    partial class AddMoreData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("_7Assist.Models.Admin", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("surname");

                    b.HasKey("IdUser")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdUser" }, "fk_admins_users_idx");

                    b.ToTable("admins", (string)null);

                    b.HasData(
                        new
                        {
                            IdUser = 3,
                            Name = "Иван",
                            Patronymic = "Иванович",
                            Surname = "Иванов"
                        });
                });

            modelBuilder.Entity("_7Assist.Models.Terminal", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("address");

                    b.HasKey("IdUser")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdUser" }, "fk_terminals_users1_idx");

                    b.ToTable("terminals", (string)null);

                    b.HasData(
                        new
                        {
                            IdUser = 1,
                            Address = "г. Архангельск"
                        },
                        new
                        {
                            IdUser = 2,
                            Address = "г. Екатеринбург"
                        });
                });

            modelBuilder.Entity("_7Assist.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.HasKey("IdUser")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Login" }, "login_UNIQUE")
                        .IsUnique();

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            IdUser = 1,
                            Login = "arhterminal",
                            Password = "$2a$11$DBc1a.5BMD3qZx4BFN1.mOv5I0BRkn7YGYadhhVdUvv546mdclqZW"
                        },
                        new
                        {
                            IdUser = 2,
                            Login = "ekbterminal",
                            Password = "$2a$11$2MXBIphNzvNHuI8lekHKE.dyAVLEeTxQKVUVH2vb5/gNrTjuaivWa"
                        },
                        new
                        {
                            IdUser = 3,
                            Login = "Ivan",
                            Password = "$2a$11$hbe8EP7TQZG6.kYzmd/QQOunf.yuHv38390O0Qneq4Q4l37MVywvS"
                        });
                });

            modelBuilder.Entity("_7Assist.Models.Admin", b =>
                {
                    b.HasOne("_7Assist.Models.User", "IdUserNavigation")
                        .WithOne("Admin")
                        .HasForeignKey("_7Assist.Models.Admin", "IdUser")
                        .IsRequired()
                        .HasConstraintName("fk_admins_users");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("_7Assist.Models.Terminal", b =>
                {
                    b.HasOne("_7Assist.Models.User", "IdUserNavigation")
                        .WithOne("Terminal")
                        .HasForeignKey("_7Assist.Models.Terminal", "IdUser")
                        .IsRequired()
                        .HasConstraintName("fk_terminals_users1");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("_7Assist.Models.User", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Terminal");
                });
#pragma warning restore 612, 618
        }
    }
}
