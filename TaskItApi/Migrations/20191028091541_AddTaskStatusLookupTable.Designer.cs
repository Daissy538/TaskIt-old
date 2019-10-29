﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskItApi.Models;

namespace TaskItApi.Migrations
{
    [DbContext(typeof(TaskItDbContext))]
    [Migration("20191028091541_AddTaskStatusLookupTable")]
    partial class AddTaskStatusLookupTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskItApi.Entities.Color", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Colors");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Pink",
                            Value = "#ec407a"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Orange",
                            Value = "#ef5350"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Purple",
                            Value = "#ab47bc"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Blue",
                            Value = "#5c6bc0"
                        });
                });

            modelBuilder.Entity("TaskItApi.Entities.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IconID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ColorID");

                    b.HasIndex("IconID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TaskItApi.Entities.Icon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Icons");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "House",
                            Value = "house"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Work",
                            Value = "work"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Sport",
                            Value = "directions_run"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Education",
                            Value = "school"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Game",
                            Value = "headset_mic"
                        },
                        new
                        {
                            ID = 6,
                            Name = "Music",
                            Value = "music_note"
                        },
                        new
                        {
                            ID = 7,
                            Name = "Nature",
                            Value = "nature_people"
                        },
                        new
                        {
                            ID = 8,
                            Name = "Voluntary work",
                            Value = "loyalty"
                        },
                        new
                        {
                            ID = 9,
                            Name = "Animal",
                            Value = "pets"
                        },
                        new
                        {
                            ID = 10,
                            Name = "Art",
                            Value = "color_lens"
                        });
                });

            modelBuilder.Entity("TaskItApi.Entities.Subscription", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfSubscription")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.HasIndex("UserID");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("TaskItApi.Entities.Task", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Until")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.HasIndex("StatusID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskItApi.Entities.TaskHolder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TaskID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TaskID");

                    b.HasIndex("UserID");

                    b.ToTable("TaskHolder");
                });

            modelBuilder.Entity("TaskItApi.Entities.TaskStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TaskStatus");
                });

            modelBuilder.Entity("TaskItApi.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskItApi.Entities.Group", b =>
                {
                    b.HasOne("TaskItApi.Entities.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskItApi.Entities.Icon", "Icon")
                        .WithMany()
                        .HasForeignKey("IconID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskItApi.Entities.Subscription", b =>
                {
                    b.HasOne("TaskItApi.Entities.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskItApi.Entities.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskItApi.Entities.Task", b =>
                {
                    b.HasOne("TaskItApi.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskItApi.Entities.TaskStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskItApi.Entities.TaskHolder", b =>
                {
                    b.HasOne("TaskItApi.Entities.Task", "Task")
                        .WithMany("TaskHolders")
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskItApi.Entities.User", "User")
                        .WithMany("TaskHolders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
