﻿using System;

namespace Domain.Entities
{
    // TODO: Change *long id* to *string id*
    public class ApplicationTask
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}