using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int EnrollmentId { get; set; }

    public decimal? MidtermScore { get; set; }

    public decimal? FinalScore { get; set; }

    public double? AverageScore { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;
}
