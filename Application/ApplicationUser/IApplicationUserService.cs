using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Common.ApplicationUser;

namespace Application.ApplicationUser
{
    public interface IApplicationUserService
    {
        Task<Domain.Entities.ApplicationUser> Add(Domain.Entities.ApplicationUser user);
        Task<Domain.Entities.ApplicationUser> GetById(string id);
        Task<UserDto> Get(LoginDto user);
        Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAll();
        Task DeleteUserById(string id);
        Task<UserDto> Update(UserDto user);
    }
}