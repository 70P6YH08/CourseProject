using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.DTOs
{
    public class InterlocutorDto
    {
        public Guid UserId { get; set; }
        public string DisplayName { get; set; } = null!;
        public DateTime? LastActionDateTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
