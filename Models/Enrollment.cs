using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public DateOnly? EnrollDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Grade? Grade { get; set; }

    public virtual Student Student { get; set; } = null!;
}
