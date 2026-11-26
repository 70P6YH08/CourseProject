using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;
using ReplicaLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.Services
{
    public class ConversationService(ReplicaDbContext replicaDbContext)
    {
        public async Task<List<ConversationDto>?> GetConversationListAsync(Guid userId)
        {
            var chats = await replicaDbContext.Chats
                .Where(c => c.UserChats.Any(uc => uc.UserId == userId))
                .Select(c => new ConversationDto
                {
                    Id = c.ChatId,
                    ConversationTypeId = c.ChatTypeId,
                    DisplayName = c.ChatType.Name == "private"
                        ? c.UserChats
                            .Where(uc => uc.UserId != userId)
                            .Select(uc => uc.User.Name ?? uc.User.Login)
                            .FirstOrDefault()
                        : c.GroupChats
                            .Select(g => g.Name).FirstOrDefault(),
                    LastMessageContent = c.ChatType.Name == "private"
                        ? c.Messages
                            .OrderByDescending(m => m.CreatedDateTime)
                            .Select(m => m.Content)
                            .FirstOrDefault()
                        : c.Messages
                            .OrderByDescending(m => m.CreatedDateTime)
                            .Select(m => $"{m.Sender.Name}: {m.Content}")
                            .FirstOrDefault(),
                    LastMessageDateTime = c.Messages
                        .OrderByDescending(m => m.CreatedDateTime)
                        .Select(m => m.CreatedDateTime)
                        .FirstOrDefault(),
                    UnreadMessageCount = c.Messages
                        .Count(m => m.SenderId != userId
                            && !m.UsersReadMessages
                                .Any(r => r.UserId == userId))
                }).ToListAsync();
            return chats;
        }
    }
}
