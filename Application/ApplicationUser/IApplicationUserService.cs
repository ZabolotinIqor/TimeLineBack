using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.ApplicationUser
{
    public interface IApplicationUserService
    {
        Task<Domain.Entities.ApplicationUser> Add(Domain.Entities.ApplicationUser user);
        Task<Domain.Entities.ApplicationUser> GetById(string id);
        Task<Domain.Entities.ApplicationUser> Get(LoginDto user);
        Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAll();
        Task DeleteUserById(string id);
        Task<Domain.Entities.ApplicationUser> Update(Domain.Entities.ApplicationUser user);
    }
}