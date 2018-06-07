using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class DaysOfWeek
    {
        public DaysOfWeek()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int DaysOfWeekId { get; set; }
        public string DaysOfWeekName { get; set; }

        public ICollection<Appointment> Appointment { get; set; }
    }
}
