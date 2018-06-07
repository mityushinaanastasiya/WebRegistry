using System;
using System.Collections.Generic;

namespace WebRegistry.Models
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public string Email { get; set; }
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}
