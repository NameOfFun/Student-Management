using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class UserAccount
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? StudentId { get; set; }

    public int? LecturerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Lecturer? Lecturer { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
