using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDSample1.Domain.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public String Username { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }


    }
}