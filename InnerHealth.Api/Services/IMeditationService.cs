using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <summary>
/// Provides operations for meditation sessions.
/// </summary>
public interface IMeditationService
{
    Task<IEnumerable<MeditationSession>> GetSessionsAsync(DateOnly date);
    Task<int> GetDailyTotalAsync(DateOnly date);
    Task<IDictionary<DateOnly, int>> GetWeeklyTotalsAsync(DateOnly weekStart);
    /// <summary>
    /// Gets the recommended daily meditation duration in minutes.
    /// </summary>
    int GetRecommendedDailyMinutes();
    Task<MeditationSession> AddSessionAsync(int minutes);
    Task<MeditationSession?> UpdateSessionAsync(int id, int minutes);
    Task<bool> DeleteSessionAsync(int id);
}