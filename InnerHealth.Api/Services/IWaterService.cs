using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <summary>
/// Provides operations for water intake.
/// </summary>
public interface IWaterService
{
    /// <summary>
    /// Gets all water intake records for a specific date.
    /// </summary>
    Task<IEnumerable<WaterIntake>> GetIntakesAsync(DateOnly date);
    /// <summary>
    /// Gets the total amount of water consumed on a given date.
    /// </summary>
    Task<int> GetDailyTotalAsync(DateOnly date);
    /// <summary>
    /// Gets a collection representing the total water consumed for each day in the specified week.
    /// </summary>
    Task<IDictionary<DateOnly, int>> GetWeeklyTotalsAsync(DateOnly weekStart);
    /// <summary>
    /// Calculates the recommended daily water intake (in millilitres) based on the user's weight.
    /// </summary>
    Task<int> GetRecommendedDailyAmountAsync();
    /// <summary>
    /// Adds a new water intake entry for today.
    /// </summary>
    Task<WaterIntake> AddIntakeAsync(int amountMl);
    /// <summary>
    /// Updates an existing water intake entry.
    /// </summary>
    Task<WaterIntake?> UpdateIntakeAsync(int id, int amountMl);
    /// <summary>
    /// Deletes an existing water intake entry.
    /// </summary>
    Task<bool> DeleteIntakeAsync(int id);
}