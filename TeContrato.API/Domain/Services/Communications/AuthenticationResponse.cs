using TeContrato.API.Domain.Models;

namespace TeContrato.API.Domain.Services.Communications
{
    public class AuthenticationResponse
    {
        public int Cuser { get; set; }
        public string Nuser { get; set; }
        public string Cpassword { get; set; }
        public string Temail { get; set; }
        public int Cdni { get; set; }
        public string Nname { get; set; }
        public string Nlastname { get; set; }
        public int is_admin { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }

        // constructor
        public AuthenticationResponse(User client, string token)
        {
            Cuser = client.Cuser;
            Temail = client.Temail;
            Token = token;
        }

        public AuthenticationResponse(string message)
        {
            Message = message;
        }

        public AuthenticationResponse() { }
    }
}