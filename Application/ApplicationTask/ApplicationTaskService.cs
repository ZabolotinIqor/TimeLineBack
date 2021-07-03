using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common.ApplicationTask;
using Infrastructure.DataBaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.ApplicationTask
{
    public class ApplicationTaskService: IApplicationTaskService
    {
        private readonly TimeLineDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationTaskService(TimeLineDbContext context,
                                      IMapper mapper,  
                                      UserManager<Domain.Entities.ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Domain.Entities.ApplicationTask> AddTask(CreateApplicationTask task,CancellationToken cts)
        {
            var mappedTask = _mapper.Map<Domain.Entities.ApplicationTask>(task);
            await _context.ApplicationTasks.AddAsync(mappedTask,cts);
            await _context.SaveChangesAsync(cts);
            return mappedTask;
        }

        public async Task<Domain.Entities.ApplicationTask> GetTaskById(long id)
        {
             var task = await _context.ApplicationTasks.FirstOrDefaultAsync(appTask => appTask.Id == id);
             return task;
        }

        public async Task<IEnumerable<Domain.Entities.ApplicationTask>> GetAllTasks()
        {
            return await _context.ApplicationTasks.ToListAsync();
        }

        public async Task DeleteTaskById(long id)
        {
            var task = await _context.ApplicationTasks.FirstOrDefaultAsync(appTask => appTask.Id == id);
            _context.ApplicationTasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.ApplicationTask> UpdateTask(UpdateApplicationTask task)
        {
            var applicationTask = await _context.ApplicationTasks.FirstOrDefaultAsync(appTask => appTask.Id == task.Id);
            applicationTask.Description = task.Description;
            applicationTask.Name = task.Name;
            applicationTask.StartDate = task.StartDate;
            applicationTask.EndDate = task.EndDate;
            applicationTask.UpdatedTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return applicationTask;
        }
    }
}