using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyProject.DAL.Data.Configurations;
using MyProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace MyProject.DAL.Data
{
	public class ProjectDbContext : IdentityDbContext <ApplicationUser>
    {


        public ProjectDbContext(DbContextOptions<ProjectDbContext> options): base(options)
        {
            
        }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee>  employees { get; set; }
       

        




    }
}
