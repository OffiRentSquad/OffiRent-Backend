using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.Api.DataObjects.Posts
{
    public class NewPost
    {
        public int OfficeId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Title { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}
