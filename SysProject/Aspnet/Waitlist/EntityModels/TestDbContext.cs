using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Waitlist.EntityModels;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CourseDatum> CourseData { get; set; }

    public virtual DbSet<StudentDatum> StudentData { get; set; }

    public virtual DbSet<WaitRequest> WaitRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=YourPasswordHere;database=database_name", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CourseDatum>(entity =>
        {
            entity.HasKey(e => new { e.CourseCode, e.Term })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("course_data");

            entity.HasIndex(e => e.CourseCode, "course_code_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Term, "term_UNIQUE").IsUnique();

            entity.Property(e => e.CourseCode)
                .HasMaxLength(45)
                .HasColumnName("course_code");
            entity.Property(e => e.Term)
                .HasMaxLength(6)
                .HasColumnName("term");
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(45)
                .HasColumnName("course_title");
            entity.Property(e => e.Credits)
                .HasPrecision(2)
                .HasColumnName("credits");
            entity.Property(e => e.Instructor)
                .HasMaxLength(45)
                .HasColumnName("instructor");
            entity.Property(e => e.Location)
                .HasMaxLength(45)
                .HasColumnName("location");
            entity.Property(e => e.Schedule)
                .HasMaxLength(45)
                .HasColumnName("schedule");
            entity.Property(e => e.Seats)
                .HasMaxLength(45)
                .HasColumnName("seats");
        });

        modelBuilder.Entity<StudentDatum>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.ToTable("student_data");

            entity.HasIndex(e => e.StudentId, "student_id_UNIQUE").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(25)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<WaitRequest>(entity =>
        {
            entity.HasKey(e => e.WaitId).HasName("PRIMARY");

            entity.ToTable("wait_request");

            entity.HasIndex(e => e.CourseCode, "course_code_fk_idx");

            entity.HasIndex(e => e.StudentId, "student_idx").IsUnique();

            entity.HasIndex(e => e.Term, "term_idx");

            entity.HasIndex(e => e.WaitId, "wait_id_UNIQUE").IsUnique();

            entity.Property(e => e.WaitId).HasColumnName("wait_id");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(45)
                .HasColumnName("course_code");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Term)
                .HasMaxLength(45)
                .HasColumnName("term");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");

            entity.HasOne(d => d.Student).WithOne(p => p.WaitRequest)
                .HasForeignKey<WaitRequest>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
