using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models;

public partial class Student
{
    public int StudentId { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public bool? Gender { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
