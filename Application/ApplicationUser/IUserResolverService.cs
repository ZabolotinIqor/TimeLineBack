using System.Threading.Tasks;

namespace Application.ApplicationUser
{
    public interface IUserResolverService
    {
        Task<Domain.Entities.ApplicationUser>  GetUser();
    }
}