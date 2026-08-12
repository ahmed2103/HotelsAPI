using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Room
{
    public class RoomResponseDto
    {
        public int RoomId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? BookedUntil { get; set; }
        public string Description { get; set; }

        public List<string> Images { get; set; } = new();

    }
}
