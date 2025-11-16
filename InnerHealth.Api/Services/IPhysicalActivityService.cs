using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <summary>
/// Provides operations for physical activities.
/// </summary>
public interface IPhysicalActivityService
{
    Task<IEnumerable<PhysicalActivity>> GetActivitiesAsync(DateOnly date);
    Task<IDictionary<DateOnly, IEnumerable<PhysicalActivity>>> GetWeeklyActivitiesAsync(DateOnly weekStart);
    Task<PhysicalActivity> AddActivityAsync(string? modality, int durationMinutes);
    Task<PhysicalActivity?> UpdateActivityAsync(int id, string? modality, int durationMinutes);
    Task<bool> DeleteActivityAsync(int id);
}