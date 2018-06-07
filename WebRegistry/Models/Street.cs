using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Street
    {
        public Street()
        {
            NospitalOnStreet = new HashSet<NospitalOnStreet>();
            Patient = new HashSet<Patient>();
        }

        public int StreetId { get; set; }
        public string StreetName { get; set; }
        public string Home { get; set; }
        public int SiteId { get; set; }

        public Site Site { get; set; }
        public ICollection<NospitalOnStreet> NospitalOnStreet { get; set; }
        public ICollection<Patient> Patient { get; set; }
    }
}
