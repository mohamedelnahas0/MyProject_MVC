using Microsoft.EntityFrameworkCore;
using MyProject.DAL.Data.Configurations;
using MyProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DAL.Data
{
    internal class ProjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
           => optionsBuilder.UseSqlServer("Server=.; Database=MVC_Project;trusted_connection=True;MultipleActiveResultSets=False");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> departments { get; set; }


    }
}
