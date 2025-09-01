using System;
using System.Collections.Generic;

namespace EF_Core_DbFirst.Models;

public partial class Student
{
    public int StudentId { get; set; }
    public int RegisterNumber { get; set; }
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
