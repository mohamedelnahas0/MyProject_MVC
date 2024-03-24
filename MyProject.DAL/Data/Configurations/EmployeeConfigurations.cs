using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50)
                .IsRequired();

            builder.Property(E=>E.Adress).IsRequired();

            builder.Property(E=>E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.gender).HasConversion
                (
                (Gender) => Gender.ToString(),
                (genderasstring)=> (Gender) Enum.Parse(typeof(Gender), genderasstring, true)
                );

        }
    }
}
