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
    [Migration("20241117134737_Initial")]
    partial class Initial
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
                            Password = "$2a$11$Ded9whnU846IfIHHyDcXAeDxaZnHop2aGb2JJBqx.teF4UJw8uFKG"
                        },
                        new
                        {
                            IdUser = 2,
                            Login = "ekbterminal",
                            Password = "$2a$11$EUKQ7Ybqn9Y.1d1k1OxYi.volMA5Q72xLALR11WxpWGf9c3o3nidy"
                        },
                        new
                        {
                            IdUser = 3,
                            Login = "Ivan",
                            Password = "$2a$11$TDEJ1bx/lHpssa4sPRyOBusuXyC6rn6VuOE8QXkTY2PCVwzf5Sw0a"
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