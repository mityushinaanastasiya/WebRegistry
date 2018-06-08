using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string PatientId { get; set; }
        public int DaysOfWeekId { get; set; }
        public DateTime DataTime { get; set; }

        public DaysOfWeek DaysOfWeek { get; set; }
        public Doctor Doctor { get; set; }
    }
}
