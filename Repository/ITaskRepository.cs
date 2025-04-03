using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementMVC.Models;

namespace TaskManagementMVC.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId);
        Task<TaskModel> GetTaskById(int taskId);
        Task<TaskModel> AddTask(TaskModel task);
        Task<bool> UpdateTask(TaskModel task);
        Task<bool> SoftDeleteTask(int taskId);
        Task<List<StatusModel>> GetStatusList();
        Task<List<PriorityModel>> GetPriorityList(int id);
    }
}
