using Microsoft.AspNetCore.Mvc;
using Models;
using Helpers;

namespace UserTaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        UserTaskCollections userTaskCollections;
        private static int autoGenerateId = 0;
        public TaskController(UserTaskCollections userTaskCollections)
        {
            this.userTaskCollections = userTaskCollections;
        }

        #region Create Task
        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem taskItems)
        {
            string errorMessage = userTaskCollections.ValidateTitle(taskItems.Title);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new { Message = errorMessage });
            }

            taskItems.Id = Interlocked.Increment(ref autoGenerateId);
            taskItems = userTaskCollections.FormatStatusAndDate(taskItems);
            userTaskCollections.taskItems.Add(taskItems);
            return Ok();
        }
        #endregion

        #region Get All Tasks
        [HttpGet]
        public IActionResult GetAllTasks(string? status = null, string? priority = null)
        {
            var tasks = userTaskCollections?.taskItems?.ToList();

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound(new { Message = $"No data found" });
            }

            if (!string.IsNullOrEmpty(status))
            {
                tasks = tasks.Where(o => o.Status.ToLower().Contains(status.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(priority))
            {
                tasks = tasks.Where(o => o.Priority.ToLower().Contains(priority.ToLower())).ToList();
            }

            if(tasks.Count==0)
            {
                return NotFound(new { Message = $"Matching task not found" });
            }
            return Ok(tasks.OrderBy(o=>o.Priority));
        }
        #endregion

        #region Get Task By Id
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = userTaskCollections.taskItems.Where(o => o.Id > 0 && o.Id == id).FirstOrDefault();
            if (task == null)
            {
                return NotFound(new { Message = $"Task Id {id} is not found" });
            }

            return Ok(task);
        }
        #endregion

        #region Update Task Status
        [HttpPut("{id}")]
        public IActionResult UpdateTaskStatus(int id)
        {
            var task = userTaskCollections.taskItems.Where(o => o.Id > 0 && o.Id == id).FirstOrDefault();
            if (task == null)
            {
                return NotFound(new { Message = $"Task Id {id} is not found" });  
            }
            else if (task.Status == StatusTypes.Completed.ToString())
            {
                return Ok(new { Message = $"Task Id is already completed" });
            }

            task.Status = userTaskCollections.UpdateStatus(task.Status);            
            return Ok(task);
        }
        #endregion
    }
}
