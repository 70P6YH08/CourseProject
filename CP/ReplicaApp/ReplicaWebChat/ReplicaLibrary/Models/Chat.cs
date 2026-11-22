using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class Chat
{
    public Guid ChatId { get; set; }

    public sbyte ChatTypeId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? EditedDateTime { get; set; }

    public virtual ChatType ChatType { get; set; } = null!;

    public virtual ICollection<GroupChat> GroupChats { get; set; } = new List<GroupChat>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();
}
