using Microsoft.Data.SqlClient;
using ParameterDB.Model;
using Parameter.Utility;
using System.Collections;
using System.Data;

namespace Parameter;
public class CRUD
{

    //READ
    public List<Student> GetAllStudents()
    {
        var students = new List<Student>();
        using (SqlConnection conn = DbOpenConnection.GetOpenConnection())
        {
            //conn.Open();
            using (SqlCommand cmd = new SqlCommand("Select * from Student;", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    int age = reader.GetInt32(3);
                    string gender = reader.GetString(4);

                    students.Add(new Student(id, firstName, lastName, age, gender));
                }

            }

        }
        return students;
    }

    //INSERT    
    public void InsertWithParameter()
    {
        try
        {
            using (SqlConnection connection = DbOpenConnection.GetOpenConnection())
            {
                //connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Student (FirstName, LastName, Age, Gender) VALUES (@firstName,@lastName,@age,@gender)", connection))
                {

                    command.Parameters.AddWithValue("@firstName", InputValidation.GetFirstName());
                    command.Parameters.AddWithValue("@lastName", InputValidation.GetLastName());
                    command.Parameters.AddWithValue("@age", InputValidation.GetAge());
                    command.Parameters.AddWithValue("@gender", InputValidation.GetGender());
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            //Console.Write(ex.ToString());
            Console.WriteLine("Unable to insert a student in to the databse...");
        }
    }

    //UPDATE
    public void UpdateWithParameter(int studentID)
    {
        try
        {
            using (SqlConnection connection = DbOpenConnection.GetOpenConnection())
            {
                //connection.Open();
                using (SqlCommand command = new SqlCommand("update student set FirstName = @firstName,LastName = @lastName,Age = @age,Gender = @gender where StudentID = @studentID", connection))
                {
                    command.Parameters.AddWithValue("@studentID", studentID);

                    command.Parameters.AddWithValue("@firstName", InputValidation.GetFirstName());
                    command.Parameters.AddWithValue("@lastName", InputValidation.GetLastName());
                    command.Parameters.AddWithValue("@age", InputValidation.GetAge());
                    command.Parameters.AddWithValue("@gender", InputValidation.GetGender());

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.ToString());
            Console.WriteLine("Error occured while update a student data in the database..");
        }
    }

    //DELETE
    public void DeleteWithParameter()
    {
        try
        {
            using (SqlConnection connection = DbOpenConnection.GetOpenConnection())
            {
                //connection.Open();
                using (SqlCommand command = new SqlCommand("delete from Student where StudentId = @studentID;", connection))
                {

                    command.Parameters.AddWithValue("@studentID", InputValidation.GetStudentID());
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            Console.WriteLine("Unable to delete a student in the databse...");
        }
    }

    public void ShowAllRecord()
    {
        var students = GetAllStudents();
        Console.WriteLine($"{"\nStudent ID",-10} {"First Name",-20} {"Last Name",-20} {"Age",-10} {"Gender",-10}\n");
        foreach (var student in students)
        {
            Console.Write($"{student.Id,-10} {student.FirstName,-20} {student.LastName,-20} {student.Age,-10} {student.Gender,-10}\n");
        }
        Console.WriteLine();
    }



    public void CallStoredProcedure()
    {
        try
        {
            var students = new List<Student>();
            var courses = new List<Course>();
            using (SqlConnection connection = DbOpenConnection.GetOpenConnection())
            {
                using (SqlCommand command = new SqlCommand("ReturnMultipleResultSet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            int age = reader.GetInt32(3);
                            string gender = reader.GetString(4);

                            students.Add(new Student(id, firstName, lastName, age, gender));

                        }
                    }
                }

                Console.WriteLine($"{"\nStudent ID",-10} {"First Name",-20} {"Last Name",-20} {"Age",-10} {"Gender",-10}\n");
                foreach (var student in students)
                {
                    Console.Write($"{student.Id,-10} {student.FirstName,-20} {student.LastName,-20} {student.Age,-10} {student.Gender,-10}\n");
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Unable to call the stored procedure ");
            Console.WriteLine(ex.ToString());
        }
    }

    public void CallStoredProcedureWithParameter(int studentID)
    {
        try
        {
            using (SqlConnection connection = DbOpenConnection.GetOpenConnection())
            {
                using (SqlCommand command = new SqlCommand("GetOneStudenDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", studentID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.GetString(1);
                            string lastName = reader.GetString(2);
                            int age = reader.GetInt32(3);
                            string gender = reader.GetString(4);

                            Console.WriteLine($"{"\nStudent ID",-10} {"First Name",-20} {"Last Name",-20} {"Age",-10} {"Gender",-10}\n");

                            Console.Write($"{id,-10} {firstName,-20} {lastName,-20} {age,-10} {gender,-10}\n\n\n");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Unable to call the stored procedure with the parameter student id");
            Console.WriteLine(ex.ToString());
        }
    }


    public void SPReturnMultipleResultSet()
    {
        try
        {
            var students = new List<Student>();
            var courses = new List<Course>();
            using (SqlConnection connection = DbOpenConnection.GetOpenConnection())
            {
                using (SqlCommand command = new SqlCommand("ReturnMultipleResultSet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int resultSetIndex = 0;
                        do
                        {
                            if (resultSetIndex == 0)
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string firstName = reader.GetString(1);
                                    string lastName = reader.GetString(2);
                                    int age = reader.GetInt32(3);
                                    string gender = reader.GetString(4);

                                    students.Add(new Student(id, firstName, lastName, age, gender));
                                }
                            }
                            else 
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string courseName = reader.GetString(1);
                                    int credits = reader.GetInt32(2);
                                    courses.Add(new Course(id, courseName, credits));
                                }
                            }
                            //else if (resultSetIndex == 1)
                            //{
                            //    while (reader.Read())
                            //    {
                            //        //Console.WriteLine(reader.GetValue(0));
                            //    }
                            //}
                            ////Console.WriteLine("index  : " + resultSetIndex);
                            resultSetIndex++;
                        } while (reader.NextResult());
                    }
                }

                Console.WriteLine($"{"\nStudent ID",-10} {"First Name",-20} {"Last Name",-20} {"Age",-10} {"Gender",-10}\n");
                foreach (var student in students)
                {
                    Console.Write($"{student.Id,-10} {student.FirstName,-20} {student.LastName,-20} {student.Age,-10} {student.Gender,-10}\n");
                }
                Console.WriteLine();

                Console.WriteLine($"{"Course ID",-10} {"Course Name",-40} {"Credits",-10}\n");
                foreach (var course in courses)
                {
                    Console.Write($"{course.Id,-10} {course.CourseName,-40} {course.Credits,-10}\n");
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Unable to call the multi result set stored procedure ");
            Console.WriteLine(ex.ToString());
        }
    }

    // SP return multiple result set handled using DataTable and DataSet
    public void SpReturnMultipleResultSetUsingDataTable()
    {
        try
        {
            using (SqlConnection conn = DbOpenConnection.GetOpenConnection())
            {
                DataSet dataSet = new DataSet();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("ReturnMultipleResultSet", conn))
                {
                    dataAdapter.Fill(dataSet);

                    dataSet.Tables[0].TableName = "student";
                    dataSet.Tables[1].TableName = "course";
                    dataSet.Tables[2].TableName = "grade";

                    Console.WriteLine("====================== STUDENT =====================");
                    foreach(DataRow row in dataSet.Tables["student"].Rows)
                    {
                        Console.WriteLine($"{row[0],-10} {row[1],-20} {row[2],-20} {row[3],-10} {row[4],-10}");
                    }

                    Console.WriteLine("====================  COURSE =====================");
                    foreach(DataRow row in dataSet.Tables["course"].Rows)
                    {
                        Console.WriteLine($"{row[0],-10} {row[1],-40} {row[2],-10}");
                    }
                }

            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Unable to read the data from the DB using Dataset and DataTable..");
        }
    }
}


