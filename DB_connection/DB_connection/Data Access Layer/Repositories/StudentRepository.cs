using Azure;
using DB_connection.Data_Access_Layer;
using DB_connection.Models;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace DB_connection.Data_Access_Layer.Repositories
{
    internal class StudentRepository
    {
        public List<Student> GetAll()
        {
            var students = new List<Student>();
            using (var conn = DbConnectionFactory.GetOpenConnection())
            using (var command = new SqlCommand("select * from Student", (SqlConnection)conn))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    int age = reader.GetInt32(3);
                    string gender = reader.GetString(4);
                    string department = reader.GetString(5);

                    students.Add(new Student(id, firstName, lastName, age, gender, department));
                }
            }
            return students;
        }

        public void AddStudent(Student student)
        {
            int id = student.Id;
            string firstName = student.FirstName;
            string lastName = student.LastName;
            int age = student.Age;
            string gender = student.Gender;
            string department = student.Department;

            using (var conn = DbConnectionFactory.GetOpenConnection())
            using (var command = new SqlCommand("Insert into Student (FirstName, LastName, Age, Gender, Department) values(@FirstName,@LastName,@Age,@Gender,@Department)", (SqlConnection)conn))
            {
                //command.Parameters.AddWithValue("@StudentID", id);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Department", department);

                command.ExecuteNonQuery();
                Console.WriteLine("The new Student added successfully......");

            }
        }


        public void UpdateStudent(int studentID)
        {
            using (var conn = DbConnectionFactory.GetOpenConnection())
            using (var command = new SqlCommand("select * from Student where StudentID = @studId", (SqlConnection)conn))
            {
                command.Parameters.AddWithValue("@studId", studentID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }

                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    int age = reader.GetInt32(3);
                    string gender = reader.GetString(4);
                    string department = reader.GetString(5);

                    reader.Close();

                    firstName = "Boobathi Raja";
                    lastName = "Ayyappan";


                    using (var updateCommand = new SqlCommand("UPDATE Student SET FirstName = @FirstName, LastName = @LastName," +
                             " Age = @Age, Gender = @Gender, Department = @Department WHERE StudentID = @StudentID", (SqlConnection)conn))
                    {
                        updateCommand.Parameters.AddWithValue("@FirstName", firstName);
                        updateCommand.Parameters.AddWithValue("@LastName", lastName);
                        updateCommand.Parameters.AddWithValue("@Age", age);
                        updateCommand.Parameters.AddWithValue("@Gender", gender);
                        updateCommand.Parameters.AddWithValue("@Department", department);
                        updateCommand.Parameters.AddWithValue("@StudentID", studentID);

                        updateCommand.ExecuteNonQuery();
                        Console.WriteLine($"The student data is updated successfullyyy........ with the id {studentID}");

                    }                                  
                }
            }
        }
    }
}