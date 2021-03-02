using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Employee_API.Models
{
    public partial class Employee_dbContext : DbContext
    {
        public Employee_dbContext()
        {
        }

        public Employee_dbContext(DbContextOptions<Employee_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MasterEmployee> MasterEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=Employee_db;Username=postgres;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterEmployee>(entity =>
            {
                entity.HasKey(e => e.PkempId)
                    .HasName("master_employee_pkey");

                entity.ToTable("master_employee");

                entity.Property(e => e.PkempId)
                    .HasColumnName("pkemp_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.EmployeeDesigination)
                    .IsRequired()
                    .HasColumnName("employee_desigination")
                    .HasMaxLength(200);

                entity.Property(e => e.EmployeeDob)
                    .HasColumnName("employee_dob")
                    .HasColumnType("date");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name")
                    .HasMaxLength(200);

                entity.Property(e => e.EmployeePhonenumber)
                    .HasColumnName("employee_phonenumber")
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
