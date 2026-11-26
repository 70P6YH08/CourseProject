namespace ReplicaLibrary.DTOs
{
    public class InterlocutorDetailsDto
    {
        public Guid UserId { get; set; }
        public string Login { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public string? ProfileDescription { get; set; }
        public DateTime? LastActionDateTime { get; set; }
        public string Status { get; set; } = null!;
    }
}
