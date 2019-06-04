using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentsReports.Domain.Models
{
    public partial class StudentsReportsContext : DbContext
    {
        public StudentsReportsContext()
        {
        }

        public StudentsReportsContext(DbContextOptions<StudentsReportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<CoursesTypes> CoursesTypes { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReportUsers> ReportUsers { get; set; }
        public virtual DbSet<StudentCourses> StudentCourses { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<TeacherCourses> TeacherCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=StudentsReports;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reports>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_dbo.Reports_dbo.Subjects_SubjectId");
            });

            modelBuilder.Entity<ReportUsers>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.UserId });

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportUsers)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK_dbo.ReportUsers_dbo.Reports_ReportId");
            });

            modelBuilder.Entity<StudentCourses>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CourseId });

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.StudentCourses_dbo.Courses_CourseId");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TeacherId).HasMaxLength(450);
            });

            modelBuilder.Entity<TeacherCourses>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.TeacherCourses_dbo.Courses_CourseId");

                entity.HasOne(d => d.CourseType)
                    .WithMany(p => p.TeacherCourses)
                    .HasForeignKey(d => d.CourseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.TeacherCourses_dbo.CourseTypes_CourseTypeId");
            });
        }
    }
}
