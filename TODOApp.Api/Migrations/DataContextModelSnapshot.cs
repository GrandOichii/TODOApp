﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TODOApp.Api.Data;

#nullable disable

namespace TODOApp.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TODOApp.Api.Models.Subtask", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserTaskID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserTaskID");

                    b.ToTable("Subtasks");
                });

            modelBuilder.Entity("TODOApp.Api.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TODOApp.Api.Models.UserTask", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Username");

                    b.ToTable("UserTasks");
                });

            modelBuilder.Entity("TODOApp.Api.Models.Subtask", b =>
                {
                    b.HasOne("TODOApp.Api.Models.UserTask", null)
                        .WithMany("Subtasks")
                        .HasForeignKey("UserTaskID");
                });

            modelBuilder.Entity("TODOApp.Api.Models.UserTask", b =>
                {
                    b.HasOne("TODOApp.Api.Models.User", null)
                        .WithMany("Tasks")
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("TODOApp.Api.Models.User", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TODOApp.Api.Models.UserTask", b =>
                {
                    b.Navigation("Subtasks");
                });
#pragma warning restore 612, 618
        }
    }
}
