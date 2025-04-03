//using Microsoft.AspNetCore.Mvc;
//using TaskManagementMVC.Models;
//using TaskManagementMVC.Repository;

//namespace TaskManagementMVC.Services
//{
//    public class ITaskServices : ITaskRepository
//    {

//        public Task<TaskModel> AddTask(TaskModel task)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TaskModel> SoftDeleteTask(int taskId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<PriorityModel>> GetPriorityList(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<StatusModel>> GetStatusList()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TaskModel> GetTaskById(int taskId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TaskModel> UpdateTask(TaskModel name)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}

using TaskManagementMVC.Models;

namespace TaskManagementMVC.Services
{
    public interface ITaskServices
    {
        Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId);
        Task<TaskModel> GetTaskById(int id);
        Task<TaskModel> AddTask(TaskModel task);
        Task<bool> UpdateTask(TaskModel task);
        Task<bool> SoftDeleteTask(int id); 
        Task<List<StatusModel>> GetStatusList();
        Task<List<PriorityModel>> GetPriorityList(int id);
    }
}
