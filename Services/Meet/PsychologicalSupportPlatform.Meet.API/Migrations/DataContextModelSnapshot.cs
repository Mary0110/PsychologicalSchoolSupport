﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data;

#nullable disable

namespace PsychologicalSupportPlatform.Meet.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PsychologicalSupportPlatform.Meet.Domain.Entities.Meetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("ScheduleCellId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleCellId");

                    b.ToTable("Meetups");
                });

            modelBuilder.Entity("PsychologicalSupportPlatform.Meet.Domain.Entities.ScheduleCell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("ScheduleCells");
                });

            modelBuilder.Entity("PsychologicalSupportPlatform.Meet.Domain.Entities.Meetup", b =>
                {
                    b.HasOne("PsychologicalSupportPlatform.Meet.Domain.Entities.ScheduleCell", "ScheduleCell")
                        .WithMany("Meetups")
                        .HasForeignKey("ScheduleCellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScheduleCell");
                });

            modelBuilder.Entity("PsychologicalSupportPlatform.Meet.Domain.Entities.ScheduleCell", b =>
                {
                    b.Navigation("Meetups");
                });
#pragma warning restore 612, 618
        }
    }
}
