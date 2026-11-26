using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;
using ReplicaLibrary.DTOs;

namespace ReplicaLibrary.Services
{
    public class InterlocutorService(ReplicaDbContext replicaDbContext)
    {
        public async Task<InterlocutorDto> GetInterlocutorAsync(Guid userId, Guid chatId)
        {
            var interlocutor = await replicaDbContext.UserChats
                .AsNoTracking()
                .Where(uc => uc.UserId != userId
                    && uc.ChatId == chatId
                    && uc.LeftDateTime == null
                    && uc.DeletedDateTime == null
                    && uc.ClearedDateTime == null
                    && uc.Chat.ChatType.Name == "private")
                .Select(uc => new InterlocutorDto
                {
                    UserId = uc.UserId,
                    DisplayName = uc.User.Name ?? uc.User.Login,
                    LastActionDateTime = uc.User.LastActionDateTime,
                    Status = uc.User.Status
                }).FirstOrDefaultAsync();

            if(interlocutor == null)
                throw new ArgumentNullException(nameof(interlocutor));

            return interlocutor;
        }

        public async Task<InterlocutorDetailsDto> GetInterlocutorDetailsAsync(Guid interlocutorId)
        {
            var interlocutorDetails = await replicaDbContext.Users
                .AsNoTracking()
                .Where(u => u.UserId == interlocutorId)
                .Select(u => new InterlocutorDetailsDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Login = u.Login,
                    BirthdayDate = u.BirthdayDate,
                    ProfileDescription = u.ProfileDescription,
                    LastActionDateTime = u.LastActionDateTime,
                    Status = u.Status
                }).FirstOrDefaultAsync();

            if (interlocutorDetails == null)
                throw new ArgumentNullException(nameof(interlocutorDetails));

            return interlocutorDetails;
        }

        public async Task<List<InterlocutorDto>> GetListInterlocutorByGroupId(Guid groupId)
        {
            var groupMembers = await replicaDbContext.UserChats
                .AsNoTracking()
                .Where(uc => uc.Chat.GroupChats
                    .Any(g => g.GroupId == groupId)
                    && uc.LeftDateTime == null
                    && uc.ClearedDateTime == null
                    && uc.DeletedDateTime == null)
                .Select(uc => new InterlocutorDto
                {
                    UserId = uc.UserId,
                    DisplayName = uc.User.Name ?? uc.User.Login,
                    LastActionDateTime = uc.User.LastActionDateTime,
                    Status = uc.User.Status
                }).ToListAsync();

            if(groupMembers is null)
                throw new ArgumentNullException(nameof(groupMembers));

            return groupMembers;
        }
    }
}
