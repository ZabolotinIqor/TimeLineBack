using System;
using System.Threading;
using System.Threading.Tasks;
using Application.ApplicationTask;
using Domain.Common;
using Domain.Common.ApplicationTask;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApplicationTaskController: ApiBaseController
    {
        private readonly IApplicationTaskService _taskService;

        public ApplicationTaskController(IApplicationTaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        [Route("/get")]
        public async Task<IActionResult> getTask(long id)
        {
            var result = await _taskService.GetTaskById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> add(CreateApplicationTask task,CancellationToken cts)
        {
            var result = await _taskService.AddTask(task,cts);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/getAll")]
        public async Task<IActionResult> getAllTask()
        {
            var result = await _taskService.GetAllTasks();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/update")]
        public async Task<IActionResult> updateTask(UpdateApplicationTask task)
        {
            var result = await _taskService.UpdateTask(task);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/delete")]
        public async Task<IActionResult> deleteTask(long id)
        {
            await _taskService.DeleteTaskById(id);
            return Ok();
        } 

    }
}