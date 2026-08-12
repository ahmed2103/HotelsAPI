using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public enum BookingStatus
    {
        Pending,//0
        Confirmed,//1
        Cancelled//2
        
    }
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; } 
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ExpiresAt { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

    }
}
