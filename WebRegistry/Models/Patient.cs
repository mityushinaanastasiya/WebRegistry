using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int StreetId { get; set; }
        public string MedicalPolicy { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Street Street { get; set; }
    }
}
