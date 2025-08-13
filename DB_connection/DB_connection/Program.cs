using System;
using DB_connection.Data_Access_Layer.Repositories;
using DB_connection.Models;

namespace DB_connection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var student_repo = new StudentRepository();
            foreach(var student in student_repo.GetAll())
            {
                Console.WriteLine($"Student ID : {student.Id}\n" +
                    $"Student First Name : {student.FirstName}\n" +
                    $"Student Last Name {student.LastName}\n" +
                    $"Age  :  {student.Age}\n" +
                    $"Gender : {student.Gender}\n" +
                    $"Department : {student.Department}\n\n");
            }
            Console.WriteLine("Its working perfectly......");


            //student_repo.AddStudent(new Student(10, "boobathi", "raja", 22, "M", "computer science"));
            //student_repo.AddStudent(new Student(10, "Guhan", "rajan", 30, "F", "computer science and Engineering"));
            Console.WriteLine("The student method called and run successfully...");


            student_repo.UpdateStudent(6);
        }
    }

}
