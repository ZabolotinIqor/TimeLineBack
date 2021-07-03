using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    // TODO: Change *long id* to *string id*
    public class ApplicationTask
    {
        [Key]
        public long Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdatedTime { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}