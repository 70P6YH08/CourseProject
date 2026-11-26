namespace ReplicaLibrary.DTOs
{
    public class GroupChatDetailsDto
    {
        public Guid ChatId { get; set; }
        public GroupDto Group { get; set; } = null!;
        public List<GroupMessageDto> Messages { get; set; } = new();
    }
}
