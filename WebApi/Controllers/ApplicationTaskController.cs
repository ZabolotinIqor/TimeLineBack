
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
        private readonly UserManager<Domain.Entities.ApplicationUser> _userManager;

        public ApplicationTaskController(IApplicationTaskService taskService,
                            UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("/getUsersTask")]
        public async Task<IActionResult> GetUsersTask(long id)
        {
            var result = await _taskService.GetTaskById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/addTask")]
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
        [Route("/addMultiplyTasks")]
        public async Task<IActionResult> AddMultiplyTasks(IEnumerable<CreateApplicationTask> tasks,CancellationToken cts)
        {
            var result = await _taskService.AddMultiplyTasks(tasks,cts);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/getAllUsersTasks")]
        public async Task<IActionResult> GetAllUsersTasks()
        {
            var result = await _taskService.GetAllTasks();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/getAllUsersTasksForToday")]
        public async Task<IActionResult> GetAllUsersTasksForToday()
        {
            var result = await _taskService.GetAllTaskForToday();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/updateUsersTask")]
        public async Task<IActionResult> UpdateUsersTask(UpdateApplicationTask task)
        {
            var result = await _taskService.UpdateTask(task);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/deleteUsersTask")]
        public async Task<IActionResult> DeleteUsersTask(long id)
        {
            await _taskService.DeleteTaskById(id);
            return Ok();
        } 

    }
}