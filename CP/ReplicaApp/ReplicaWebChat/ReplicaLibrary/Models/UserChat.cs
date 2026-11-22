using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class UserChat
{
    public Guid UserId { get; set; }

    public Guid ChatId { get; set; }

    public sbyte RoleId { get; set; }

    public DateTime JoinedDateTime { get; set; }

    public DateTime? LeftDateTime { get; set; }

    public DateTime? DeletedDateTime { get; set; }

    public DateTime? ClearedDateTime { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
