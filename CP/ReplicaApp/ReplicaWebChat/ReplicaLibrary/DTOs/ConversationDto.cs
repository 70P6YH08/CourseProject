using ReplicaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.DTOs
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public sbyte ConversationTypeId { get; set; }
        public string? DisplayName { get; set; }
        public string? LastMessageContent { get; set; } = null!;
        public DateTime LastMessageDateTime { get; set; }
        public int UnreadMessageCount { get; set; }
    }
}
