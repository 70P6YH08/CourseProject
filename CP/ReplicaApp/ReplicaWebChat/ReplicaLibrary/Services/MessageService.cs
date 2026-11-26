using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;
using ReplicaLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.Services
{
    public class MessageService(ReplicaDbContext replicaDbContext)
    {
        public async Task<List<PrivateMessageDto>> GetPrivateMessagesByChatIdAsync(
            Guid userId, Guid interlocutorId, Guid chatId)
        {
            var messages = await replicaDbContext.Messages
                .AsNoTracking()
                .Where(m => m.ChatId == chatId
                    && m.Chat.UserChats
                    .Any(uc => uc.UserId == userId
                        && uc.LeftDateTime == null
                        && uc.DeletedDateTime == null
                        && uc.ClearedDateTime == null)
                    && m.Chat.ChatType.Name == "private")
                .Select(m => new PrivateMessageDto
                {
                    MessageId = m.MessageId,
                    Content = m.Content,
                    CreatedDateTime = m.CreatedDateTime,
                    Status = m.SenderId == userId
                        && m.UsersReadMessages
                            .Any(r => r.UserId == interlocutorId)
                })
                .OrderByDescending(m => m.CreatedDateTime)
                .Take(100)
                .ToListAsync();

            return messages;
        }

        public async Task<List<GroupMessageDto>> GetGroupMessagesByChatIdAsync(
            Guid userId, Guid chatId)
        {
            var messages = await replicaDbContext.Messages
                .AsNoTracking()
                .Where(m => m.ChatId == chatId
                    && m.Chat.UserChats
                    .Any(uc => uc.UserId == userId
                        && uc.LeftDateTime == null
                        && uc.DeletedDateTime == null
                        && uc.ClearedDateTime == null)
                    && m.Chat.ChatType.Name == "group")
                .Select(m => new GroupMessageDto
                {
                    MessageId = m.MessageId,
                    SenderId = m.SenderId,
                    SenderName = m.Sender.Name ?? m.Sender.Login,
                    Content = m.Content,
                    CreatedDateTime = m.CreatedDateTime,
                    ReadByCount = m.UsersReadMessages.Count()
                })
                .OrderByDescending(m => m.CreatedDateTime)
                .Take(100)
                .ToListAsync();

            return messages;
        }
    }
}
