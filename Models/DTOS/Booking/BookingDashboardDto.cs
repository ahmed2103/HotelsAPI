using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Booking
{
    public class BookingDashboardDto
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string GuestName { get; set; }
        public string Room { get; set; }

        public string RoomNumber { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; }
    }
}

