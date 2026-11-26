using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.DTOs
{
    public class InsertUserDto
    {
        public Guid UserId { get; set; } = new Guid();
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime RegistrationDateTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Active";
    }
}
