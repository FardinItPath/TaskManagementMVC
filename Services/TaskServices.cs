//using Microsoft.AspNetCore.Mvc;
//using TaskManagementMVC.Models;
//using TaskManagementMVC.Repository;

//namespace TaskManagementMVC.Services
//{
//    public class TaskServices : ITaskServices
//    {
//           private readonly ITaskRepository _taskRepository;
//        public TaskServices(ITaskRepository taskRepository) 
//        { 

//        _taskRepository = taskRepository;
//        }

//        public async Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId) => await _taskRepository.GetTasksByUserId(userId);
//        public async Task<TaskModel> GetTaskById(int id) => await _taskRepository.GetTaskById(id);
//        public async Task<TaskModel> AddTask(TaskModel task) => await _taskRepository.AddTask(task);
//        public async Task<TaskModel> UpdateTask(TaskModel task) => await _taskRepository.UpdateTask(task);
//        public async Task<TaskModel> SoftDeleteTask(int id) => await _taskRepository.SoftDeleteTask(id);
//        public async Task<List<StatusModel>> GetStatusList() => await _taskRepository.GetStatusList();
//        public async Task<List<PriorityModel>> GetPriorityList(int id) => await _taskRepository.GetPriorityList(id);
//    }
//}
using TaskManagementMVC.Models;
using TaskManagementMVC.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementMVC.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly ITaskRepository _taskRepository;

    
        public TaskServices(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId) =>
             await _taskRepository.GetTasksByUserId(userId);

        public async Task<TaskModel> GetTaskById(int id) =>
            await _taskRepository.GetTaskById(id);

        public async Task<TaskModel> AddTask(TaskModel task) =>
            await _taskRepository.AddTask(task);

        public async Task<bool> UpdateTask(TaskModel task) =>
            await _taskRepository.UpdateTask(task);

        public async Task<bool> SoftDeleteTask(int taskId)
        {
            return await _taskRepository.SoftDeleteTask(taskId);
        }

        public async Task<List<StatusModel>> GetStatusList() =>
            await _taskRepository.GetStatusList();

        public async Task<List<PriorityModel>> GetPriorityList(int id) =>
            await _taskRepository.GetPriorityList(id);




    }
}
