using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Site
    {
        public Site()
        {
            Doctor = new HashSet<Doctor>();
            Street = new HashSet<Street>();
        }

        public int SiteId { get; set; }
        public string SiteNumber { get; set; }

        public ICollection<Doctor> Doctor { get; set; }
        public ICollection<Street> Street { get; set; }
    }
}
