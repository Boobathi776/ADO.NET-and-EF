using System;
using System.Collections.Generic;

namespace EF_Core_DbFirst.Models;

public partial class CourseAssignment
{
    public int AssignmentId { get; set; }

    public int CourseId { get; set; }

    public int TeacherId { get; set; }

    public DateOnly AssignedDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
