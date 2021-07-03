using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<ApplicationTask> Tasks { get; set; }
    }
}