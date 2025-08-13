using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterDB.Model;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public Student(int id, string firstName, string lastName, int age, string gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
    }
}
