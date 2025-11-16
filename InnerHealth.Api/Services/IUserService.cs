using InnerHealth.Api.Dtos;
using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <summary>
/// Provides operations related to the user profile.
/// </summary>
public interface IUserService
{
    Task<UserProfile?> GetUserAsync();
    Task<UserProfile> UpdateUserAsync(UpdateUserProfileDto dto);
}