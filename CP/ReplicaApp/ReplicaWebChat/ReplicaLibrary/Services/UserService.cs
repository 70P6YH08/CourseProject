using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Contexts;
using ReplicaLibrary.DTOs;

namespace ReplicaLibrary.Services
{
    public class UserService(ReplicaDbContext replicaDbContext)
    {
        public async Task<CurrentUserDto> GetCurrentUserAsync(Guid userId)
        {
            var user = await replicaDbContext.Users
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .Select(u => new CurrentUserDto
                {
                    UserId = userId,
                    Login = u.Login,
                    Name = u.Name,
                    ProfileDescription = u.ProfileDescription,
                    Email = u.Email,
                    BirthdayDate = u.BirthdayDate,
                    LastActionDateTime = u.LastActionDateTime,
                    Status = u.Status
                }).FirstOrDefaultAsync();

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return user;
        }
    }
}
