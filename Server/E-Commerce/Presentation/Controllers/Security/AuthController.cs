using Application.Interfaces.Security;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using Presentation.DTOs.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ITokenClaimService _tokenService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public AuthController(IAuthService accountService, ITokenClaimService tokenService, IMapper mapper, ICurrentUserService currentUserService)
        {
            _authService = accountService;
            _tokenService = tokenService;
            _mapper = mapper;
            _currentUserService = currentUserService;

        }

        [HttpPost]
 
        public async Task<TokenResponse> Auth(AuthRequestDTO auth)
        {
            try
            {
                var user = await _authService.AuthenticateAsync(auth.UserName, auth.Password);
                if (user == null)
                {
                    return new TokenResponse(null, null, "Invalid login", false);
                }
                var token = await _tokenService.GetTokenAsync(user);
                if (token == null)
                {
                    return new TokenResponse(null, null, "Cant Generate Token", false);

                }
                return new TokenResponse(_mapper.Map<UserDTO>(user), token, "Successfully Logged in ");
            }catch (Exception ex)
            {
                return new TokenResponse(null, null, "Invalid login", false);
            }

        }

           
        [Authorize]
        [HttpGet("CurrentUser")]
        public async  Task<Response<UserLoginInfoDTO>> GetLoginUserAsync()
        {
            var user =  _currentUserService.UserInfo;
            return  new Response<UserLoginInfoDTO>(_mapper.Map<UserLoginInfoDTO>(user));
        }
    }
}
