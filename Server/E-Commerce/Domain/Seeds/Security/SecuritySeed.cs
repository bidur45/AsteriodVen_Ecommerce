using Domain.Entities.Security;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Seeds.Security
{
    public static class SecuritySeed
    {
   
        public static List<User> Users = new List<User>()
    {
        new User("05a9b8bd-e08b-4493-b7fd-f47602b63ca8", "Superuser@gmail.com", "Superuser123", "05a9b8bd-e08b-4493-b7fd-f47602b63ca8", EnStatus.Active),
    };

    

    }
}
