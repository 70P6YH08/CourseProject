namespace ReplicaLibrary.DTOs
{
    public class PrivateMessageDto
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public bool Status { get; set; }
    }
}
