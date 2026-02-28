using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? OfficeLocation { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
