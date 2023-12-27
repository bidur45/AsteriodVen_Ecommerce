namespace Presentation.DTOs.Security
{
    public class AuthRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AuthRequestDTO(string userName, string password) { 
        
        UserName = userName;
          Password = password;    
        }

    }
}
