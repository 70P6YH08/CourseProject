namespace ReplicaLibrary.DTOs
{
    public class PrivateChatDetailsDto
    {
        public Guid ChatId { get; set; }
        public InterlocutorDto User { get; set; } = null!;
        public List<PrivateMessageDto> Messages { get; set; } = new();
    }
}
