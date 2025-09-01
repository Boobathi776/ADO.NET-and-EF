using System;
using System.Collections.Generic;

namespace EF_Core_DbFirst.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int EnrollmentId { get; set; }

    public string Grade1 { get; set; } = null!;

    public string? Remarks { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;
}
