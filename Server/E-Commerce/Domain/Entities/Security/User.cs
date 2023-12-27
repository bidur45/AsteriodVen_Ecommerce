
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Security
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
 

        public User() { }
        public User(string id,  string userName,string password, string entryBy,  EnStatus status)       
        { 
        
            Id= Guid.Parse(id);
            UserName= userName;
            Password = password;
            EntryById = Guid.Parse(entryBy);
            Status = status;

        }

    }
}
