using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDSample1.Domain.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public Username Username { get; set; }
        public Role Role { get; set; }
        public Email Email { get; set; }


    }
}