//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace EF_Core_DbFirst
//{
//    public class DesignTimeFactory:IDesignTimeDbContextFactory<StudentDbContext>
//    {
//        public StudentDbContext CreateDbContext(string[] args)
//        {
//            var config = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", optional: false)
//                .Build();

//            var options = new DbContextOptionsBuilder<StudentDbContext>()
//                .UseSqlServer(config.GetConnectionString("StudentDB"))
//                .Options;

//            return new StudentDbContext(options);
//        }
//    }
//}
