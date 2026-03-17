using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TMIApi.DTOs;
using TMIApi.Models;
using TMIApi.Repositories;

namespace TMIApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public TaskService(ITaskRepository taskRepository, IMapper mapper,IMemoryCache cache)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<TaskResponseDto>> GetAllAsync(int userId)
        {
            var cacheKey = $"tasks_user_{userId}";
            if(_cache.TryGetValue(cacheKey, out List<TaskResponseDto> cachedTasks))
            {
                return cachedTasks;
            }
            var tasks = await _taskRepository.GetAllAsync(userId);
            _cache.Set(cacheKey, _mapper.Map<List<TaskResponseDto>>(tasks), TimeSpan.FromMinutes(5));    
            return _mapper.Map<List<TaskResponseDto>>(tasks);
        
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
           var cacheKey = $"task_{id}";
           if(_cache.TryGetValue(cacheKey,out TaskItem cachedTask))
           {
            return cachedTask;
           }
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("No task found");
            _cache.Set(cacheKey, _mapper.Map<TaskItem>(task), TimeSpan.FromMinutes(5));
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
            _cache.Remove($"tasks_user_{task.UserId}"); 
            _cache.Remove($"task_{id}");  
        }

        
    }
}