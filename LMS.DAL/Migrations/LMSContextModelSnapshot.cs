﻿// <auto-generated />
using System;
using LMS.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS.DAL.Migrations
{
    [DbContext(typeof(LMSContext))]
    partial class LMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMS.DAL.Entity.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Events", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Course_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<double>("Max_Degree")
                        .HasColumnType("float");

                    b.Property<double>("Min_Degree")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Course_ID");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Chat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstCos_ID")
                        .HasColumnType("int");

                    b.Property<int>("Std_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstCos_ID");

                    b.HasIndex("Std_ID");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("LMS.DAL.Entity.InstructorCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Course_ID")
                        .HasColumnType("int");

                    b.Property<int>("inst_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Course_ID");

                    b.HasIndex("inst_ID");

                    b.ToTable("InstructorCourse");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Instructors", b =>
                {
                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userID");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Questions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Exam_ID")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("questionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Exam_ID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("LMS.DAL.Entity.StudentExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Exam_ID")
                        .HasColumnType("int");

                    b.Property<int>("Std_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Exam_ID");

                    b.HasIndex("Std_ID");

                    b.ToTable("StudentExam");
                });

            modelBuilder.Entity("LMS.DAL.Entity.StudentQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Ques_ID")
                        .HasColumnType("int");

                    b.Property<int>("Std_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Ques_ID");

                    b.HasIndex("Std_ID");

                    b.ToTable("StudentQuestion");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Students", b =>
                {
                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMS.DAL.Entity.UserEvent", b =>
                {
                    b.Property<int>("user_ID")
                        .HasColumnType("int");

                    b.Property<int>("event_ID")
                        .HasColumnType("int");

                    b.HasKey("user_ID", "event_ID");

                    b.HasIndex("event_ID");

                    b.ToTable("UserEvent");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Exam", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Courses", "Courses")
                        .WithMany("Exam")
                        .HasForeignKey("Course_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Group", b =>
                {
                    b.HasOne("LMS.DAL.Entity.InstructorCourse", "InstructorCourse")
                        .WithMany("Group")
                        .HasForeignKey("InstCos_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.DAL.Entity.Students", "Students")
                        .WithMany("Group")
                        .HasForeignKey("Std_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InstructorCourse");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMS.DAL.Entity.InstructorCourse", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Courses", "Courses")
                        .WithMany("InstructorCourse")
                        .HasForeignKey("Course_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.DAL.Entity.Instructors", "Instructors")
                        .WithMany("InstructorCourse")
                        .HasForeignKey("inst_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Instructors");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Instructors", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Users", "Users")
                        .WithOne()
                        .HasForeignKey("LMS.DAL.Entity.Instructors", "userID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Questions", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("Exam_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("LMS.DAL.Entity.StudentExam", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Exam", "Exam")
                        .WithMany("StudentExam")
                        .HasForeignKey("Exam_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.DAL.Entity.Students", "Students")
                        .WithMany("StudentExam")
                        .HasForeignKey("Std_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMS.DAL.Entity.StudentQuestion", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Questions", "Questions")
                        .WithMany("StudentQuestion")
                        .HasForeignKey("Ques_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.DAL.Entity.Students", "Students")
                        .WithMany("StudentQuestion")
                        .HasForeignKey("Std_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questions");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Students", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Users", "Users")
                        .WithOne()
                        .HasForeignKey("LMS.DAL.Entity.Students", "userID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LMS.DAL.Entity.UserEvent", b =>
                {
                    b.HasOne("LMS.DAL.Entity.Events", "Events")
                        .WithMany("UserEvent")
                        .HasForeignKey("event_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.DAL.Entity.Users", "Users")
                        .WithMany("UserEvent")
                        .HasForeignKey("user_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Courses", b =>
                {
                    b.Navigation("Exam");

                    b.Navigation("InstructorCourse");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Events", b =>
                {
                    b.Navigation("UserEvent");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Exam", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("StudentExam");
                });

            modelBuilder.Entity("LMS.DAL.Entity.InstructorCourse", b =>
                {
                    b.Navigation("Group");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Instructors", b =>
                {
                    b.Navigation("InstructorCourse");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Questions", b =>
                {
                    b.Navigation("StudentQuestion");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Students", b =>
                {
                    b.Navigation("Group");

                    b.Navigation("StudentExam");

                    b.Navigation("StudentQuestion");
                });

            modelBuilder.Entity("LMS.DAL.Entity.Users", b =>
                {
                    b.Navigation("UserEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
