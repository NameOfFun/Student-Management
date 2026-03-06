using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserAccount> Users { get; set; } = new List<UserAccount>();
}
