using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class Role
{
    public sbyte RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();
}
