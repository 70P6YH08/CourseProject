using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime BirthdayDate { get; set; }

    public string? ProfileDescription { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public DateTime? EditedDateTime { get; set; }

    public DateTime? DeletedDateTime { get; set; }

    public DateTime? LastActionDateTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>();

    public virtual ICollection<UsersDeletedMessage> UsersDeletedMessages { get; set; } = new List<UsersDeletedMessage>();

    public virtual ICollection<UsersReadMessage> UsersReadMessages { get; set; } = new List<UsersReadMessage>();
}
