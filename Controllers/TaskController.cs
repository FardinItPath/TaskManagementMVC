using Microsoft.AspNetCore.Mvc;
using TaskManagementMVC.Models;
using TaskManagementMVC.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagementMVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskServices _taskServices;

        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = await _taskServices.GetTasksByUserId(userId.Value);
            return View(tasks);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.StatusList = await GetStatusList();
            ViewBag.PriorityList = await GetPriorityList();

            return View(new TaskModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StatusList = await GetStatusList();
                ViewBag.PriorityList = await GetPriorityList();
                return View(task);
            }

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            task.UserId = userId.Value;
            task.CreatedBy = userId.Value;
            task.CreatedDT = DateTime.Now;

            await _taskServices.AddTask(task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskServices.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.StatusList = await GetStatusList();
            ViewBag.PriorityList = await GetPriorityList();

            return View(task);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StatusList = await GetStatusList();
                ViewBag.PriorityList = await GetPriorityList();
                return View(task);
            }

            task.UpdatedBy = HttpContext.Session.GetInt32("UserId") ?? 1; 

            task.CreatedDT = DateTime.Now;

            await _taskServices.UpdateTask(task);
            return RedirectToAction("Index");
        }

        [HttpGet("Task/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        
            var isDeleted = await _taskServices.SoftDeleteTask(id);
            if (isDeleted)
            {
                return RedirectToAction("Index"); 
            }
            return NotFound();
        }


        private async Task<List<SelectListItem>> GetStatusList()
        {
            var statusList = await _taskServices.GetStatusList();
            return statusList.Select(s => new SelectListItem
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusName
            }).ToList();
        }

        private async Task<List<SelectListItem>> GetPriorityList()
        {
            var priorityList = await _taskServices.GetPriorityList(0);
            return priorityList.Select(p => new SelectListItem
            {
                Value = p.PriorityId.ToString(),
                Text = p.PriorityName
            }).ToList();
        }
    }
}
