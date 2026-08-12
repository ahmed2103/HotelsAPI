using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Hotel
    {
        public int HotelId { set; get; }
        public string Name { set; get; } = string.Empty;
        public ICollection<Review>? Reviews { set; get; }
    }
}
