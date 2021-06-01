using System.Collections.Generic;
using OffiRent.Domain.Models.Geographic;
using OffiRent.Domain.Models.Identity;

namespace OffiRent.Domain.Models.Booking
{
    public class Office : AuditModel
    {
        public Office()
        {
            Posts = new HashSet<Post>();
            Reservations = new HashSet<Reservation>();
        }
        
        public ICollection<Post> Posts { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }

        public District District { get; set; }
        public int DistrictId { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        /// <summary>
        /// This Office is currently in an on-going Reservation
        /// </summary>
        public bool Busy { get; set; }
        
    }
}