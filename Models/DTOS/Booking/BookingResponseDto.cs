using Models.DTOS.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Booking
{
    public class BookingResponseDto
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }        // اللي حجز
        public int RoomId { get; set; }
        public RoomResponseDto Room { get; set; } // علشان نعرض بيانات الغرفة المرتبطة بالحجز
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? ExpiresAt { get; set; }
        public string Status { get; set; }
    }
}
