﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240520110633_UpdateCourseIdToString")]
    partial class UpdateCourseIdToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.CourseContentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Strings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseContents", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.CourseEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AltText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BestSeller")
                        .HasColumnType("bit");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CourseContentId")
                        .HasColumnType("int");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LengthInHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfReviews")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CourseContentId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.CourseIncludesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FACode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseIncludes", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.ProgramDetailsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("ProgramDetails", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.SavedCoursesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBookmarked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsJoined")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SavedCourses", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.CourseEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Infrastructure.Entities.CourseContentEntity", "CourseContent")
                        .WithMany()
                        .HasForeignKey("CourseContentId");

                    b.Navigation("Category");

                    b.Navigation("CourseContent");
                });

            modelBuilder.Entity("Infrastructure.Entities.CourseIncludesEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.CourseEntity", "Course")
                        .WithMany("CourseIncludes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Infrastructure.Entities.ProgramDetailsEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.CourseEntity", "Course")
                        .WithMany("ProgramDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Infrastructure.Entities.CourseEntity", b =>
                {
                    b.Navigation("CourseIncludes");

                    b.Navigation("ProgramDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
