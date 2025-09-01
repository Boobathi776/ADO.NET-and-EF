using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.Migrations;

namespace Practice.Model
{
    public class EmployeeContext : DbContext
    {
        // DbSets = Tables
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        // Constructor calls base with connection string name
        public EmployeeContext() : base("EmployeeContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmployeeContext,Migrations.Configuration>());
        }
    }
}


public class EmployeeDbInitializer
{
    
}
