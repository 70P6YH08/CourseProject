using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;
using ReplicaLibrary.DTOs;

namespace ReplicaLibrary.Services
{
    public class ChatService(
        ReplicaDbContext replicaDbContext,
        InterlocutorService interlocutorService,
        MessageService messageService,
        GroupService groupService)
    {
        public async Task<PrivateChatDetailsDto> GetPrivateChatDetailsAsync(Guid chatId, Guid userId)
        {
            var chat = await replicaDbContext.Chats
                .AsNoTracking()
                .Where(c => c.ChatId == chatId
                    && c.UserChats.Any(uc => uc.UserId == userId
                        && uc.LeftDateTime == null
                        && uc.DeletedDateTime == null
                        && uc.ClearedDateTime == null))
                .Select(c => new PrivateChatDetailsDto
                {
                    ChatId = chatId
                }).FirstOrDefaultAsync();

            if (chat is null)
                throw new InvalidOperationException(nameof(chat));

            chat.User = await interlocutorService.GetInterlocutorAsync(userId, chatId);

            if (chat.User is null)
                throw new InvalidOperationException($"{nameof(chat)} is null");

            chat.Messages = await messageService.GetPrivateMessagesByChatIdAsync(userId, chat.User.UserId, chatId);
            return chat;
        }

        public async Task<GroupChatDetailsDto> GetGroupChatDetailsAsync(Guid chatId, Guid userId)
        {
            var chat = await replicaDbContext.Chats
                .AsNoTracking()
                .Where(c => c.ChatId == chatId
                    && c.UserChats.Any(uc => uc.UserId == userId
                        && uc.LeftDateTime == null
                        && uc.DeletedDateTime == null
                        && uc.ClearedDateTime == null))
                .Select(c => new GroupChatDetailsDto
                {
                    ChatId = chatId
                }).FirstOrDefaultAsync();

            if (chat is null)
                throw new InvalidOperationException(nameof(chat));

            chat.Group = await groupService.GetGroupAsync(userId, chatId);

            if (chat.Group is null)
                throw new InvalidOperationException($"{nameof(chat)} is null");

            chat.Messages = await messageService.GetGroupMessagesByChatIdAsync(userId, chatId);
            return chat;
        }
    }
}
