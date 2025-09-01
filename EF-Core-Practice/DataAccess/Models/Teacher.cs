using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
}
