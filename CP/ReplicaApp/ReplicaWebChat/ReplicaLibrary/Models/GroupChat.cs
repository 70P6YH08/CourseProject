using System;
using System.Collections.Generic;

namespace ReplicaLibrary.Models;

public partial class GroupChat
{
    public Guid GroupId { get; set; }

    public Guid ChatId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Chat Chat { get; set; } = null!;
}
