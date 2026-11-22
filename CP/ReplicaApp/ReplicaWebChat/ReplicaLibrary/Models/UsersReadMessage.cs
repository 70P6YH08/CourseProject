using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class UsersReadMessage
{
    public Guid MessageId { get; set; }

    public Guid UserId { get; set; }

    public DateTime ReadDateTime { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
