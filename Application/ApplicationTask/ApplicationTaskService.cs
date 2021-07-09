using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.ApplicationUser;
using AutoMapper;
using Domain.Common.ApplicationTask;
using Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Application.ApplicationTask
{
    public class ApplicationTaskService: IApplicationTaskService
    {
        private readonly TimeLineDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserResolverService _userResolver;

        public ApplicationTaskService(TimeLineDbContext context,
                                      IMapper mapper,
                                      IUserResolverService userResolver
        )
        {
            _context = context;
            _mapper = mapper;
            _userResolver = userResolver;
        }
        public async Task<Domain.Entities.ApplicationTask> AddTask(CreateApplicationTask task, CancellationToken cts)
        {
            var mappedTask = _mapper.Map<Domain.Entities.ApplicationTask>(task);
            mappedTask.applicationUser = await _userResolver.GetUser();
            await _context.ApplicationTasks.AddAsync(mappedTask,cts);
            await _context.SaveChangesAsync(cts);
            return mappedTask;
        }

        public async Task<IEnumerable<Domain.Entities.ApplicationTask>> AddMultiplyTasks(IEnumerable<CreateApplicationTask> task, CancellationToken cts)
        {
            var mappedTask = _mapper.Map<List<Domain.Entities.ApplicationTask>>(task);
            var currentUser = await _userResolver.GetUser();
            foreach (var applicationTask in mappedTask)
            {
                applicationTask.applicationUser = currentUser;
            }

            await _context.ApplicationTasks.AddRangeAsync(mappedTask, cts);
            await _context.SaveChangesAsync(cts);
            return mappedTask;
        }
        public async Task<Domain.Entities.ApplicationTask> GetTaskById(long id)
        {
            var currentUser = await _userResolver.GetUser();
             var task = await _context.ApplicationTasks
                 .FirstOrDefaultAsync(appTask => appTask.Id == id 
                                                 && appTask.applicationUser == currentUser);
             return task;
        }

        public async Task<IEnumerable<Domain.Entities.ApplicationTask>> GetAllTasks()
        {
            var currentUser = await _userResolver.GetUser();
            return _context.ApplicationTasks.Where(task => task.applicationUser == currentUser);
        }

        public async Task DeleteTaskById(long id)
        {
            var currentUser = await _userResolver.GetUser();
            var task = await _context.ApplicationTasks
                .FirstOrDefaultAsync(appTask => appTask.Id == id && appTask.applicationUser == currentUser);
            if (task !=  null)
            {
                _context.ApplicationTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Domain.Entities.ApplicationTask> UpdateTask(UpdateApplicationTask task)
        {
            var currentUser = await _userResolver.GetUser();
            var applicationTask = await _context.ApplicationTasks
                .FirstOrDefaultAsync(appTask => appTask.Id == task.Id 
                                                && appTask.applicationUser == currentUser);
            if (applicationTask == null) return null;
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