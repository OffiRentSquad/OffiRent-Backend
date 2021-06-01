namespace OffiRent.Api.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public class AzureAdB2C
    {
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string SignUpSignInPolicyId { get; set; }
    }
}