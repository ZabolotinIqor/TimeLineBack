using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.ApplicationTask;

namespace Application.ApplicationTask
{
    public interface IApplicationTaskService
    {
        Task<Domain.Entities.ApplicationTask> AddTask(CreateApplicationTask task,CancellationToken cts);
        Task<IEnumerable<Domain.Entities.ApplicationTask>> AddMultiplyTasks(IEnumerable<CreateApplicationTask> task,CancellationToken cts);
        Task<Domain.Entities.ApplicationTask> GetTaskById(long id);
        Task<IEnumerable<Domain.Entities.ApplicationTask>> GetAllTasks();
        Task DeleteTaskById(long id);
        Task<Domain.Entities.ApplicationTask> UpdateTask(UpdateApplicationTask user);
    }
}