using Microsoft.EntityFrameworkCore;
using InnerHealth.Api.Dtos;
using InnerHealth.Api.Models;
using InnerHealth.Api.Data;

namespace InnerHealth.Api.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves the single user profile. If it does not exist, an empty profile is created.
    /// </summary>
    public async Task<UserProfile?> GetUserAsync()
    {
        var user = await _context.UserProfiles.FirstOrDefaultAsync();
        return user;
    }

    /// <summary>
    /// Updates the single user profile. Creates one if none exists.
    /// </summary>
    public async Task<UserProfile> UpdateUserAsync(UpdateUserProfileDto dto)
    {
        var user = await _context.UserProfiles.FirstOrDefaultAsync();
        if (user == null)
        {
            user = new UserProfile();
            _context.UserProfiles.Add(user);
        }
        user.Weight = dto.Weight;
        user.Height = dto.Height;
        user.Age = dto.Age;
        user.SleepQuality = dto.SleepQuality;
        user.SleepHours = dto.SleepHours;
        await _context.SaveChangesAsync();
        return user;
    }
}