using Microsoft.EntityFrameworkCore;
using TMIApi.Data;
using TMIApi.Models;

namespace TMIApi.Repositories
{
    public class TaskItemRepository : ITaskRepository
    {
        private readonly AppDbContext _db;
        public TaskItemRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
    public async  Task<List<TaskItem>> GetAllAsync(int userId)
    {
       return await _db.Tasks.Where(t => t.UserId == userId).ToListAsync();      
    }
    public async Task<TaskItem> GetByIdAsync(int id)
    {
        var tasks = await _db.Tasks.Where(t=>t.Id == id).FirstOrDefaultAsync();
        return tasks;
    }
    public async Task AddAsync(TaskItem task)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();    
    }
    public async Task UpdateAsync(TaskItem task)
    {
        var foundtask = await _db.Tasks.Where(t=>t.Id == task.Id).FirstOrDefaultAsync();
        if(foundtask == null)
            {
                throw new Exception("No task Found");
            }
            else
            {
                foundtask.Title = task.Title;
                foundtask.Description = task.Description;
                foundtask.Status = task.Status;
                
            }
            await _db.SaveChangesAsync();
    }
    public async Task DeleteAsync(TaskItem task)
    {
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();    
        }
    }
}