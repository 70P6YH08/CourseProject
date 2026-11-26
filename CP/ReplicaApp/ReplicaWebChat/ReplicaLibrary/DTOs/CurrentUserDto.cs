using ReplicaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.DTOs
{
    public class CurrentUserDto
    {
        public Guid UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string? ProfileDescription { get; set; }
        public DateTime? LastActionDateTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
