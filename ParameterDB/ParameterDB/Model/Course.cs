using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterDB.Model;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public int Credits { get; set; }

    public Course(int id, string courseName, int credits)
    {
        Id = id;
        CourseName = courseName;   
        Credits = credits;
    }
}


