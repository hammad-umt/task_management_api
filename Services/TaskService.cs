using AutoMapper;
using TMIApi.DTOs;
using TMIApi.Models;
using TMIApi.Repositories;

namespace TMIApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<List<TaskResponseDto>> GetAllAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllAsync(userId);
            return _mapper.Map<List<TaskResponseDto>>(tasks);
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("No task found");
            return _mapper.Map<TaskItem>(task);
        }

        public async Task CreateAsync(CreateTaskDto dto, int userId)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.UserId = userId;
            task.CreatedAt = DateTime.Now;
            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateAsync(int id, UpdateTaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Task nahi mila");
            _mapper.Map(dto, task);
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Task nahi mila");
            await _taskRepository.DeleteAsync(task);
        }

        
    }
}