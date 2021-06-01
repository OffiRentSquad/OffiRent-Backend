namespace OffiRent.Api.DataObjects.Office
{
    public class NewOffice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DistrictId { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}