using System.Data;
using System.Threading.Tasks;
using Dapper;
using TaskManagementMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace TaskManagementMVC.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection _db;
        private string _connectionString;

        public TaskRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId)
        {
            string sql = @"
        SELECT 
                t.TaskId, 
                t.UserId, 
                t.Title, 
                t.Description, 
                s.StatusName AS StatusId,
                p.PriorityName AS PriorityId, 
                t.DueDate, 
                t.IsActive, 
                t.CreatedBy, 
                t.CreatedDT
        FROM Tasks t
        INNER JOIN Status s ON t.StatusId = s.StatusId
        INNER JOIN Priority p ON t.PriorityId = p.PriorityId
        WHERE t.UserId = @UserId AND t.IsActive = 1";
            return await _db.QueryAsync<TaskModel>(sql, new { UserId = userId });
        }

        public async Task<TaskModel> GetTaskById(int taskId)
        {
            string sql = @"
    SELECT 
        t.TaskId, 
        t.UserId, 
        t.Title, 
        t.Description, 
        s.StatusName AS StatusId, 
        p.PriorityName AS PriorityId,
        t.DueDate, 
        t.IsActive, 
        t.CreatedBy, 
        t.CreatedDT
    FROM Tasks t  
    INNER JOIN Status s ON t.StatusId = s.StatusId
    INNER JOIN Priority p ON t.PriorityId = p.PriorityId
    WHERE t.TaskID = @TaskId";

            return await _db.QueryFirstOrDefaultAsync<TaskModel>(sql, new { TaskId = taskId });
        }


        public async Task<TaskModel> AddTask(TaskModel task)
        {
                var sql = @"
                INSERT INTO Tasks (UserId, Title, Description, StatusId, PriorityId, DueDate, IsActive, CreatedBy, CreatedDT)
                VALUES (@UserId, @Title, @Description, @StatusId, @PriorityId, @DueDate, 1, @CreatedBy, @CreatedDT);
                SELECT * FROM Tasks WHERE TaskId = SCOPE_IDENTITY();";

                return await _db.QuerySingleAsync<TaskModel>(sql, task);
            
        }



        public async Task<bool> UpdateTask(TaskModel task)
        {
            string sql = @"UPDATE Tasks 
                           SET Title = @Title, Description = @Description, StatusId = @StatusId, 
                           DueDate = @DueDate, PriorityId = @PriorityId,UpdatedBy = @UpdatedBy, 
                           UpdatedDT = GETDATE()
                           WHERE TaskId = @TaskId";
            int rowsAffected = await _db.ExecuteAsync(sql, task);
            return rowsAffected > 0;
        }
 

        public async Task<bool> SoftDeleteTask(int taskId)
        {
            string sql = "UPDATE Tasks SET IsActive = 0 WHERE TaskId = @TaskId";
            int rowsAffected = await _db.ExecuteAsync(sql, new { TaskId = taskId });
            return rowsAffected > 0;
        }

        public async Task<List<StatusModel>> GetStatusList()
        {
            string sql = "SELECT * FROM Status";
            return (await _db.QueryAsync<StatusModel>(sql)).ToList();
        }

        public async Task<List<PriorityModel>> GetPriorityList(int id)
        {
            string sql = "SELECT * FROM Priority";
            return (await _db.QueryAsync<PriorityModel>(sql)).ToList();
        }
    }
}
