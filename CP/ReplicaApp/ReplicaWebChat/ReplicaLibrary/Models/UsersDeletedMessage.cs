using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class UsersDeletedMessage
{
    public Guid UsersUserId { get; set; }

    public Guid MessagesMessageId { get; set; }

    public DateTime DeletedDateTime { get; set; }

    public virtual Message MessagesMessage { get; set; } = null!;

    public virtual User UsersUser { get; set; } = null!;
}
