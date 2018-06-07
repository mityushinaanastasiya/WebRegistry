using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            Doctor = new HashSet<Doctor>();
        }

        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }

        public ICollection<Doctor> Doctor { get; set; }
    }
}
