using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class RoomImage
    {
        public int Id { set; get; }
        
        public string ImageUrl { set; get; } = string.Empty;
        //FK
        public int RoomId { set; get; }
        public Room? Room { set; get; }
    }
}
