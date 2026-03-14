using TMIApi.DTOs;
using TMIApi.Models;

namespace TMIApi.Services
{
    public interface ITaskService
    {
        Task<List<TaskResponseDto>> GetAllAsync(int userId);
        Task<TaskItem> GetByIdAsync(int id) ;
        Task CreateAsync(CreateTaskDto dto, int userId);
        Task UpdateAsync(int id, UpdateTaskDto dto);
        Task DeleteAsync(int id);
    }
}