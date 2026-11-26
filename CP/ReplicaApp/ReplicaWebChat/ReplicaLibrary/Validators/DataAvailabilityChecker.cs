using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;

namespace PractLibrary.Services
{
    public class DataAvailabilityChecker(ReplicaDbContext context)
    {

        private async Task<(bool isAvailable, string? error)> IsAvailableLoginAsync(string login, Guid? userId = null)
            => !await context.Users
                    .AnyAsync(u => u.Login == login && (userId == null || u.UserId != userId))
                        ? (true, null)
                        : (false, "Такой логин занят");
        private async Task<(bool isAvailable, string? error)> IsAvailableEmailAsync(string email, Guid? userId = null)
            => !await context.Users
                    .AnyAsync(u => u.Email == email && (userId == null || u.UserId != userId))
                        ? (true, null)
                        : (false, "Такая почта занята");

        public async Task<(bool isAvailable, string? error)> CheckUserDataAvailabilityAsync(
            string login, string email, Guid? userId = null)
        {

            var loginAvailable = await IsAvailableLoginAsync(login, userId);
            if (!loginAvailable.isAvailable)
                return loginAvailable;

            var emailAvailable = await IsAvailableEmailAsync(email, userId);
            if (!emailAvailable.isAvailable)
                return emailAvailable;

            return (true, null);
        }
    }
}
