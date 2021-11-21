
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.ApplicationTask;
using Domain.Common.ApplicationTask;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class ApplicationTaskController: ApiBaseController
    {
        private readonly IApplicationTaskService _taskService;

        public ApplicationTaskController(IApplicationTaskService taskService,
                            UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _taskService = taskService;
        }
        [HttpGet]
        [Route("/task/{taskId}")]
        public async Task<IActionResult> GetTaskByUserId(long taskId)
        {
            var result = await _taskService.GetTaskById(taskId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/task")]
        public async Task<IActionResult> AddTask(CreateApplicationTask task,CancellationToken cts)
        {
            var result = await _taskService.AddTask(task,cts);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/tasks")]
        public async Task<IActionResult> AddTasks(IEnumerable<CreateApplicationTask> tasks,CancellationToken cts)
        {
            var result = await _taskService.AddMultiplyTasks(tasks,cts);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var result = await _taskService.GetAllTasks();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/today-tasks")]
        public async Task<IActionResult> GetTasksForToday()
        {
            var result = await _taskService.GetAllTaskForToday();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/task")]
        public async Task<IActionResult> UpdateTask(UpdateApplicationTask task)
        {
            var result = await _taskService.UpdateTask(task);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/task/{taskId}")]
        public async Task<IActionResult> DeleteUsersTask(long taskId)
        {
            await _taskService.DeleteTaskById(taskId);
            return Ok();
        } 

    }
}