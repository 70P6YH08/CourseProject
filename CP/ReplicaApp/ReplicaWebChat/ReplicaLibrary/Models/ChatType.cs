using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class ChatType
{
    public sbyte TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}
