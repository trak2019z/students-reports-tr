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
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<StudentCourses> StudentCourses { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<TeacherCourses> TeacherCourses { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

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

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportUsers)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK_dbo.ReportUsers_dbo.Reports_ReportId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.ReportUsers_dbo.Users_UserId");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<StudentCourses>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CourseId });

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.StudentCourses_dbo.Courses_CourseId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.StudentCourses_dbo.Users_UserId");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Subjects_dbo.Users_UserId");
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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeacherCourses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.TeacherCourses_dbo.Users_UserId");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.UserRoles_dbo.Roles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserRoles_dbo.Users_UserId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });
        }
    }
}
