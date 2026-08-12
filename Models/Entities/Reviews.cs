using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; } 
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int Rating { get; set; } // e.g., 1 to 5
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
