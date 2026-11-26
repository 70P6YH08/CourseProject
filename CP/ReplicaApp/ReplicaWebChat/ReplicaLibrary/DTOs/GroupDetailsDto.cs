namespace ReplicaLibrary.DTOs
{
    public class GroupDetailsDto
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string? GroupDescription { get; set; }
        public int MembersCount { get; set; }
        public List<InterlocutorDto> GroupMembers { get; set; } = new();
    }
}
