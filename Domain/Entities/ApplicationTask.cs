using System;

namespace Domain.Entities
{
    public class ApplicationTask
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdDateTime{ get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}