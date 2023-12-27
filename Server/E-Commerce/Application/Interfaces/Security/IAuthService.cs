using Domain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Security
{
    public interface IAuthService
    {

        public Task<User> AuthenticateAsync(string Username, string Password);

    }
}
