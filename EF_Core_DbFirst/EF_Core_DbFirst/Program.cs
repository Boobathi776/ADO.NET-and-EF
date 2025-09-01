
//using EF_Core_DbFirst.Models;
//using Microsoft.Data.SqlClient;

//namespace EF_Core_DbFirst
//{

//    class Program
//    {
//        static void Main()
//        {
//            //var connStr = "Server=BSD-BOOBATHIA01\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";
//            //using var conn = new SqlConnection(connStr);
//            //conn.Open();
//            //Console.WriteLine("Connected Successfully!");
//            //using (var context = new StudentDbContext())
//            //{
//            //    var students = context.Students.ToList();

//            //    foreach (var s in students)
//            //    {
//            //        Console.WriteLine($"{s.StudentId} - {s.FirstName}");
//            //    }
//            //}

//            var connectionString = builder.Configuration.GetConnectionString("StudentDB");
//            builder.Services.AddDbContext<StudentDbContext>(options =>
//                options.UseSqlServer(connectionString));

//        }
//    }

//}



using EF_Core_DbFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
// change this to the namespace where EF scaffold put your context/entities:
namespace EF_Core_DbFirst;

internal class Program
{
    static void Main(string[] args)
    {
        // Load appsettings.json (use BaseDirectory so it works when run from bin/)
        //var config = new ConfigurationBuilder()
        //    .SetBasePath(AppContext.BaseDirectory)
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .Build();

        //var connString = config.GetConnectionString("StudentDB");
        //Console.WriteLine($"Using DB: {connString}");

        //var options = new DbContextOptionsBuilder<StudentDbContext>()
        //    .UseSqlServer(connString)
        //    .Options;

        //using var db = new StudentDbContext(options);

        //// quick smoke test
        //var students = db.Students.Take(5).ToList();
        //foreach (var s in students)
        //    Console.WriteLine($"{s.StudentId} - {s.FirstName}");

        using (var context = new StudentDbContext())
        {
            var students = context.Students.ToList();
            foreach (var s in students)
            {
                Console.WriteLine($"{s.StudentId} - {s.FirstName}");
            }
        }


    }
}

