using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class StudentManagementContext : DbContext
{
    public StudentManagementContext()
    {
    }

    public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        var strConn = config["ConnectionStrings:MyCnn"];
        optionsBuilder.UseSqlServer(strConn);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__FDF47986EC0D8A5A");

            entity.ToTable("Class");

            entity.HasIndex(e => e.CourseId, "idx_class_course");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");
            entity.Property(e => e.Semester).HasColumnName("semester");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class__course_id__31EC6D26");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Classes)
                .HasForeignKey(d => d.LecturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class__lecturer___32E0915F");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__8F1EF7AE4CBB4756");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
            entity.Property(e => e.Credits).HasColumnName("credits");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Course__departme__2E1BDC42");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__C2232422F637277C");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(20)
                .HasColumnName("department_name");
            entity.Property(e => e.OfficeLocation)
                .HasMaxLength(30)
                .HasColumnName("office_location");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__6D24AA7ADF76FEB3");

            entity.ToTable("Enrollment");

            entity.HasIndex(e => new { e.StudentId, e.ClassId }, "UQ__Enrollme__55EC4103AF05DE6F").IsUnique();

            entity.HasIndex(e => e.ClassId, "idx_enrollment_class");

            entity.HasIndex(e => e.StudentId, "idx_enrollment_student");

            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.EnrollDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("enroll_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__class__38996AB5");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__stude__37A5467C");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grade__3A8F732C4E670761");

            entity.ToTable("Grade");

            entity.HasIndex(e => e.EnrollmentId, "UQ__Grade__6D24AA7B2AABF0A7").IsUnique();

            entity.Property(e => e.GradeId).HasColumnName("grade_id");
            entity.Property(e => e.AverageScore).HasColumnName("average_score");
            entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");
            entity.Property(e => e.FinalScore)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("final_score");
            entity.Property(e => e.MidtermScore)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("midterm_score");

            entity.HasOne(d => d.Enrollment).WithOne(p => p.Grade)
                .HasForeignKey<Grade>(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grade__enrollmen__3C69FB99");
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.LecturerId).HasName("PK__Lecturer__D4D1DAB1EBC2DE06");

            entity.ToTable("Lecturer");

            entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Lecturers)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lecturer__depart__2B3F6F97");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__2A33069A8D83858A");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Email, "UQ__Student__AB6E6164913BB347").IsUnique();

            entity.HasIndex(e => e.DepartmentId, "idx_student_department");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasDefaultValue(true)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(16)
                .HasColumnName("phone");

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__departm__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
