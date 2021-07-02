using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Application.ApplicationUser
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly TimeLineDbContext _context;
        public ApplicationUserService(TimeLineDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Entities.ApplicationUser> Add(Domain.Entities.ApplicationUser user)
        {
            await _context.ApplicationUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Domain.Entities.ApplicationUser> GetById(string id)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.Id == id);
            return user;
        }

        public async Task<Domain.Entities.ApplicationUser> Get(LoginDto loginDto)
        {
            var user = new Domain.Entities.ApplicationUser();
            user = await _context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.Email == loginDto.email);
            return user;
        }

        public async Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAll()
        {
            return await _context.ApplicationUsers.ToListAsync();
           
        }

        public async Task DeleteUserById(string id)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.Id == id);
            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.ApplicationUser> Update(Domain.Entities.ApplicationUser user)
        {
            var applicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.Id == user.Id);
            applicationUser = user;
            await _context.SaveChangesAsync();
            return applicationUser;
        }
    }
}