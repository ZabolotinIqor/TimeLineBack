using Domain.Enums;

namespace Domain.Entities
{
    public class ApplicationUser
    {
        public long id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public string birthDate { get; set; }
        public Role role { get; set; }
        public string password { get; set; }
    }
}