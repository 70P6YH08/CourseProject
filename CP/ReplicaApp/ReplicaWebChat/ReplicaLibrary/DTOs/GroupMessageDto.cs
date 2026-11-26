namespace ReplicaLibrary.DTOs
{
    public class GroupMessageDto
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDateTime { get; set; }
        public int ReadByCount { get; set; }
    }
}
