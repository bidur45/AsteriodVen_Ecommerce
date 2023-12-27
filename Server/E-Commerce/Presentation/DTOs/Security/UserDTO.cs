namespace Presentation.DTOs.Security
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        // Add a parameterless constructor or a constructor with optional arguments
        public UserDTO()
        {
        }

        // Alternatively, add a constructor with optional arguments
        public UserDTO(string userName = null, string password = null)
        {
            UserName = userName;
            Password = password;
        }
    }
}
