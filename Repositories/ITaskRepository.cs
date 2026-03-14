using TMIApi.Models;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(int userId);
    Task<TaskItem> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}