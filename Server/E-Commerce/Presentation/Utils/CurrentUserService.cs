using Application.Interfaces;
using Domain.Entities.Security;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Utils
{
    public class CurrentUserService:ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId =>Guid.Parse(GetClaimValue("UserId"));
        public string Name => GetClaimValue("Name");
      
        public User UserInfo => new User()
        {
            UserName = Name,
            Id = UserId,
          
        };



        private string GetClaimValue(string claimType)
        {
            return (bool)_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ? _httpContextAccessor.HttpContext?.User?.Claims.SingleOrDefault(c => c.Type == claimType)?.Value : string.Empty;
        }
    }
}
