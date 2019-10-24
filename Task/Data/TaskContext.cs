using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Task.Models;

namespace Task.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext()
        {
            Database.EnsureCreated();
        }

        //Connection settings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var connectionString = builder.Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskDb;Trusted_Connection=True;");
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder mBuilder)
        {
            mBuilder.Entity<Employee>(entity =>
            {
                entity.Property(p => p.FirstName).HasMaxLength(20);
                entity.Property(p => p.LastName).HasMaxLength(20);
                entity.Property(p => p.Patronymic).HasMaxLength(20);
                entity.Property(p => p.BirthDate).HasColumnType("date");

                entity.HasOne(p => p.Branch).WithMany(d => d.Employees).HasForeignKey(p => p.BranchId);
            });
            mBuilder.Entity<Branch>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.Manager).WithOne(d => d.BranchManager).HasForeignKey<Branch>(p => p.ManagerId);
            });
        }
    }
}
