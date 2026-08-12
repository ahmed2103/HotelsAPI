using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.DashboardStats
{
    public class DashboardStatsDto
    {
        public int TotalBookings { get; set; }
        public int Rooms { get; set; }
        public int Users { get; set; }
        public decimal Revenue { get; set; }
    }
}
