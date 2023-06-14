namespace HospitalAPI.Models.Accounts
{
    public class AuthenticationDto
    {
        public string Login { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}