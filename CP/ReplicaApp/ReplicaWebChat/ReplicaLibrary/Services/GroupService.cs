using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;
using ReplicaLibrary.DTOs;

namespace ReplicaLibrary.Services
{
    public class GroupService(
        ReplicaDbContext replicaDbContext,
        InterlocutorService interlocutorService
        )
    {
        public async Task<GroupDto> GetGroupAsync(Guid userId, Guid chatId)
        {
            var onlineSince = DateTime.Now.AddMinutes(-1);

            var group = await replicaDbContext.Chats
                .Where(c => c.UserChats
                    .Any(uc => uc.UserId == userId
                        && uc.LeftDateTime == null
                        && uc.DeletedDateTime == null
                        && uc.ClearedDateTime == null)
                    && c.ChatId == chatId
                    && c.ChatType.Name == "group")
                .Select(c => new GroupDto
                {
                    GroupId = c.GroupChats
                        .Select(g => g.GroupId)
                        .FirstOrDefault(),
                    GroupName = c.GroupChats
                        .Select(f => f.Name)
                        .FirstOrDefault()!,
                    MembersCount = c.UserChats
                        .Count(uc => uc.LeftDateTime == null
                            && uc.DeletedDateTime == null
                            && uc.ClearedDateTime == null),
                    OnlineMembersCount = c.UserChats
                        .Count(uc => uc.LeftDateTime == null
                            && uc.DeletedDateTime == null
                            && uc.ClearedDateTime == null
                            && uc.User.LastActionDateTime != null
                            && uc.User.LastActionDateTime >= onlineSince)
                }).FirstOrDefaultAsync();

            if (group == null)
                throw new ArgumentNullException(nameof(group));

            return group;
        }

        public async Task<GroupDetailsDto> GetGroupDetailsAsync(Guid groupId)
        {
            var groupDetails = await replicaDbContext.GroupChats
                .Where(g => g.GroupId == groupId)
                .Select(g => new GroupDetailsDto
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    GroupDescription = g.Description,
                    MembersCount = g.Chat.UserChats
                        .Count(uc => uc.LeftDateTime == null
                            && uc.DeletedDateTime == null
                            && uc.ClearedDateTime == null),
                }).FirstOrDefaultAsync();

            if (groupDetails == null)
                throw new ArgumentNullException(nameof(groupDetails));

            groupDetails.GroupMembers = await interlocutorService.GetListInterlocutorByGroupId(groupDetails.GroupId);

            return groupDetails;
        }
    }
}
