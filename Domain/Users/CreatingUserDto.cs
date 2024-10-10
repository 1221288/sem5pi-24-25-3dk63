using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Users;

namespace DDDSample1.Users
{
    public class CreatingUserDto
    {
        public Username Username { get; set; }
        public Role Role { get; set; }
        public Email Email { get; set; }

        public CreatingUserDto(Username username, Role role, Email email)
        {
            this.Username = username;
            this.Role = role;
            this.Email = email;
        }

    }
}