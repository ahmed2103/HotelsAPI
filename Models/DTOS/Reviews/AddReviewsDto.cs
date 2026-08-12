using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Reviews
{
    public class AddReviewsDto
    {
        public int Rating { get; set; } // e.g., 1 to 5
        public string? Comment { get; set; }
    }
}
