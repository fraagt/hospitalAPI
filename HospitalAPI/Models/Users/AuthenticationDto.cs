namespace HospitalAPI.Models.Users
{
    public class AuthenticationDto
    {
        public string Login { get; set; } = null!;

        public string Token { get; set; } = null!;
        
        public int IdRole { get; set; }
    }
}