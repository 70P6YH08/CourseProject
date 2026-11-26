namespace ReplicaLibrary.DTOs
{
    public class GroupDto
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int MembersCount { get; set; }
        public int OnlineMembersCount { get; set; }
    }
}
