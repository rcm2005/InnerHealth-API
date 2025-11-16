using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <summary>
/// Provides operations for sunlight sessions.
/// </summary>
public interface ISunlightService
{
    Task<IEnumerable<SunlightSession>> GetSessionsAsync(DateOnly date);
    Task<int> GetDailyTotalAsync(DateOnly date);
    Task<IDictionary<DateOnly, int>> GetWeeklyTotalsAsync(DateOnly weekStart);
    /// <summary>
    /// Gets the recommended daily sunlight duration in minutes.
    /// </summary>
    int GetRecommendedDailyMinutes();
    Task<SunlightSession> AddSessionAsync(int minutes);
    Task<SunlightSession?> UpdateSessionAsync(int id, int minutes);
    Task<bool> DeleteSessionAsync(int id);
}