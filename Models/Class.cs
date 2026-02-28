using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public int CourseId { get; set; }

    public int LecturerId { get; set; }

    public int? Semester { get; set; }

    public int? Year { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Lecturer Lecturer { get; set; } = null!;
}
