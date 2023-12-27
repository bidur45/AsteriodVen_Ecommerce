using Application.Interfaces;
using Application.Interfaces.Security;
using Domain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Security
{
    public class AuthService:IAuthService
    {

        private readonly IUnitOfWork _uow; 
        public AuthService(IUnitOfWork uow) {
        _uow = uow;
        }


        public async Task<User> AuthenticateAsync(string UserName , string Password)
        {
            try
            {
                var user = await _uow.GenericRepository<User>().GetFirstOrDefault<User>(
                    predicate: x => x.UserName == UserName && x.Password == Password,
                        selector: user => user

                    );
                return user;
            }catch (Exception ex)
            {
                throw ex;
            }
        }    


    }
}
