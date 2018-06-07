using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            Doctor = new HashSet<Doctor>();
            NospitalOnStreet = new HashSet<NospitalOnStreet>();
        }

        public int HospitalId { get; set; }
        public string HospitalName { get; set; }

        public ICollection<Doctor> Doctor { get; set; }
        public ICollection<NospitalOnStreet> NospitalOnStreet { get; set; }
    }
}
