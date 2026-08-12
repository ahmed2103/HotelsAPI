using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Room
{
    public class CreateRoomDto
    {
        public string Number { set; get; } = string.Empty;
        public string Type { set; get; } = string.Empty;
        public decimal PricePerNight { set; get; }
        public bool IsAvailable { set; get; }
        public string Description { set; get; }=string.Empty;
        public List<IFormFile> ? Images { set; get; } 

    }
}
