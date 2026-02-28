using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Lecturer
{
    public int LecturerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public int DepartmentId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department Department { get; set; } = null!;
}
