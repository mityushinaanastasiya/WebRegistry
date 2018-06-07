using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
            Like = new HashSet<Like>();
        }

        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int SpecialtyId { get; set; }
        public int SiteId { get; set; }
        public int HospitalId { get; set; }

        public Hospital Hospital { get; set; }
        public Site Site { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<Like> Like { get; set; }
    }
}
