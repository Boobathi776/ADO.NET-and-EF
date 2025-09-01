using Dapper;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

//using EF_Core_Practice.Model;
using Microsoft.Extensions.Configuration;

namespace EF_Core_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            using (var context = new StudentDbContext())
            {
                var conn = context.Database.GetDbConnection();
                conn.Open();
                //conn.Open();

                var course = new Course() { CourseName = "nothing" };
                Console.WriteLine(context.Entry(course).State);

                context.Entry(course).State = EntityState.Added;
                Console.WriteLine(context.Entry(course).State);

                string sql = "select * from course";
                var courses = conn.Query<Course>(sql);
                foreach (Course course1 in courses)
                {
                    Console.WriteLine(course1.CourseName);
                }
                //    var courses = context.Courses.First();
                //Console.WriteLine(courses.CourseName);

                //foreach (var course in courses)
                //{
                //    Console.WriteLine(course.CourseName);
                //}
            }
        }
    }
}
