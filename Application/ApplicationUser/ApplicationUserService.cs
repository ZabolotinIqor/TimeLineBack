using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common;
using Domain.Common.ApplicationUser;
using Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Application.ApplicationUser
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly TimeLineDbContext _context;
        private readonly IMapper _mapper;
        public ApplicationUserService(TimeLineDbContext context,
                                      IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<UserDto> Get(LoginDto loginDto)
        {
            var user = new Domain.Entities.ApplicationUser();
            user = await _context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.Email == loginDto.email);
            return _mapper.Map<UserDto>(user);
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

        public async Task<UserDto> Update(UserDto user)
        {
            var applicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(appUser => appUser.Id == user.Id);
            applicationUser = _mapper.Map<Domain.Entities.ApplicationUser>(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}