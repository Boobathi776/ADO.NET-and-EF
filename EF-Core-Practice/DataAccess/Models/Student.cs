using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public int RegisterNumber { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
