namespace OffiRent.Domain.Models.Geographic
{
    public class District : AuditModel
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}