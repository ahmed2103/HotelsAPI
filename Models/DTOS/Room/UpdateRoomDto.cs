using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Room
{
    public class UpdateRoomDto
    {
        public string Number { set; get; } = string.Empty;
        public string Type { set; get; } = string.Empty;
        public decimal PricePerNight { set; get; }
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { set; get; }

        // (replace all images)

        public List<IFormFile> Images { set; get; } = new();
    }
}
