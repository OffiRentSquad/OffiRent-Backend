namespace OffiRent.Api.DataObjects.Authentication
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(Domain.Models.Identity.User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.Surname;
            FullName = user.FullName;
            Token = token;
        }
    }
}