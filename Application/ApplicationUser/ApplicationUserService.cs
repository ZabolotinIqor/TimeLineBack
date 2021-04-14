using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Application.ApplicationUser
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly TimeLineDbContext context;
        public ApplicationUserService(TimeLineDbContext context)
        {
            this.context = context;
        }
        public async Task<Domain.Entities.ApplicationUser> Add(Domain.Entities.ApplicationUser user)
        {
            await context.ApplicationUsers.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<Domain.Entities.ApplicationUser> GetById(long id)
        {
            var user = await context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.id == id);
            return user;
        }

        public async Task<Domain.Entities.ApplicationUser> Get(LoginDto loginDto)
        {
            var user = new Domain.Entities.ApplicationUser();
            user = await context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.login == loginDto.login);
            return user;
        }

        public async Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAll()
        {
            var result =  await context.ApplicationUsers.ToListAsync();
            return result;
        }

        public async Task DeleteUserById(long id)
        {
            var user = await context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.id == id);
            context.ApplicationUsers.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.ApplicationUser> Update(Domain.Entities.ApplicationUser user)
        {
            var applicationUser = await context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.id == user.id);
            applicationUser = user;
            await context.SaveChangesAsync();
            return applicationUser;
        }
    }
}