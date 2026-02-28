using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department Department { get; set; } = null!;
}
