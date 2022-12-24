﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolServer.Data;

#nullable disable

namespace SchoolServer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221222105921_AddTableCompletedTasks")]
    partial class AddTableCompletedTasks
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SchoolServer.Data.Entities.CompletedTaskDal", b =>
                {
                    b.Property<long>("TaskDalId")
                        .HasColumnType("bigint");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer")
                        .HasColumnName("taskId");

                    b.Property<long>("UserDalId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userId");

                    b.HasIndex("TaskDalId");

                    b.HasIndex("UserDalId");

                    b.ToTable("usersTasks");
                });

            modelBuilder.Entity("SchoolServer.Data.Entities.DifficultyDal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("difficulties");
                });

            modelBuilder.Entity("SchoolServer.Data.Entities.SubjectDal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("SchoolServer.Data.Entities.TaskDal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("answer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<long>("DifficultyDalId")
                        .HasColumnType("bigint");

                    b.Property<int>("DifficultyId")
                        .HasColumnType("integer")
                        .HasColumnName("difficultyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("SubjectDalId")
                        .HasColumnType("bigint");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer")
                        .HasColumnName("subjectId");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyDalId");

                    b.HasIndex("SubjectDalId");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("SchoolServer.Data.Entities.UserDal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("SchoolServer.Data.Entities.CompletedTaskDal", b =>
                {
                    b.HasOne("SchoolServer.Data.Entities.TaskDal", "TaskDal")
                        .WithMany()
                        .HasForeignKey("TaskDalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolServer.Data.Entities.UserDal", "UserDal")
                        .WithMany()
                        .HasForeignKey("UserDalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskDal");

                    b.Navigation("UserDal");
                });

            modelBuilder.Entity("SchoolServer.Data.Entities.TaskDal", b =>
                {
                    b.HasOne("SchoolServer.Data.Entities.DifficultyDal", "DifficultyDal")
                        .WithMany()
                        .HasForeignKey("DifficultyDalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolServer.Data.Entities.SubjectDal", "SubjectDal")
                        .WithMany()
                        .HasForeignKey("SubjectDalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DifficultyDal");

                    b.Navigation("SubjectDal");
                });
#pragma warning restore 612, 618
        }
    }
}
