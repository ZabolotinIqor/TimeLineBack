﻿using System.Threading.Tasks;
using Domain.Common;

namespace Application.Authorization
{
    public interface IAuthorizationService
    {
        Task<LoginResponseDto> Login(LoginDto request);
        Task Logout();
    }
}