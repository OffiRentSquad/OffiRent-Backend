using System.Collections.Generic;

namespace OffiRent.Domain.Models.Geographic
{
    public class Country : AuditModel
    {
        public Country()
        {
            Districts = new HashSet<District>();
        }
        
        public string Name { get; set; }
        public IEnumerable<District> Districts { get; set; }
    }
}