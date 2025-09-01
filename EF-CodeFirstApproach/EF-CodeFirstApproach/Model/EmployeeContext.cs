using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirstApproach.Model
{

    public class EmployeeDbIntializer : CreateDatabaseIfNotExists<EmployeeContext>
    {
        public EmployeeDbIntializer() { }

        //Seeding the data
        protected override void Seed(EmployeeContext context)
        {
            //Add or update method only accecpts either a lamda expresssion or direct list of values
            context.Departments.AddOrUpdate(
                d=>d.DepartmentName,
                new Department() { DepartmentName = "IT" },
                new Department() { DepartmentName = "HR" },
                new Department() { DepartmentName = "Finance" },
                new Department() { DepartmentName = "System Admin" },
                new Department() { DepartmentName = "Integration" }
                );
            base.Seed( context );
            //context.SaveChanges();

        }
    }

    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("EmployeeContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmployeeContext, Migrations.Configuration>());
            Database.SetInitializer(new EmployeeDbIntializer());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        //fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Employee
            //Primary ky
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeID);

            //Employee -> department one to many 
            //modelBuilder.Entity<Employee>()
            //    .HasRequired(e => e.Department)
            //    .WithMany(d => d.Employees)
            //    .HasForeignKey(e => e.DepartmentID);  //Foreign key property

            //Employee -> EmployeeProjects
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeProjects)
                .WithRequired(ep => ep.Employee)
                .HasForeignKey(ep => ep.EmployeeID); 


            //Department
            //Primary key
            modelBuilder.Entity<Department>().HasKey(d =>d.DepartmentID);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithRequired(e => e.Department) //employee with department
                .HasForeignKey(e => e.DepartmentID); //employee using foreign key

            //Project
            //Primary key
            modelBuilder.Entity<Project>().HasKey(p=>p.ProjectID);

            modelBuilder.Entity<Project>()
                .HasMany( p => p.EmployeeProjects)
                .WithRequired(ep => ep.Project)
                .HasForeignKey(ep => ep.ProjectID);


            //composite key for Employee project entity
            modelBuilder.Entity<EmployeeProject>().HasKey(ep => new { ep.EmployeeID, ep.ProjectID });


            base.OnModelCreating(modelBuilder);
        }
    }
}
