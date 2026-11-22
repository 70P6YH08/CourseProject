using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class Message
{
    public Guid MessageId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ChatId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime? EditedDateTime { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;

    public virtual ICollection<UsersDeletedMessage> UsersDeletedMessages { get; set; } = new List<UsersDeletedMessage>();

    public virtual ICollection<UsersReadMessage> UsersReadMessages { get; set; } = new List<UsersReadMessage>();
}
