using InnerHealth.Api.Models;

namespace InnerHealth.Api.Services;

/// <summary>
/// Provides operations for task items.
/// </summary>
public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync(DateOnly date);
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task<TaskItem> AddTaskAsync(string title, string? description, DateOnly date, int? priority);
    Task<TaskItem?> UpdateTaskAsync(int id, string title, string? description, DateOnly date, bool isComplete, int? priority);
    Task<bool> DeleteTaskAsync(int id);
}