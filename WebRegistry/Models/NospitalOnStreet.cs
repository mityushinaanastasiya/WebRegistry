using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class NospitalOnStreet
    {
        public int Id { get; set; }
        public int StreetId { get; set; }
        public int HospitalId { get; set; }

        public Hospital Hospital { get; set; }
        public Street Street { get; set; }
    }
}
