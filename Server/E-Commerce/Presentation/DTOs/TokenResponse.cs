using Presentation.DTOs.Security;

namespace Presentation.DTOs
{
    public class TokenResponse
    {

        public UserDTO User { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TokenResponse(UserDTO user, string token, string message, bool isSuccess = true)
        {
            User = user;
            Token = token;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
