namespace Domain.Entities
{
    public class Settings
    {
        public long id { get; set; }
        public ApplicationUser user { get; set; }
        public string settings { get; set; }
    }
}