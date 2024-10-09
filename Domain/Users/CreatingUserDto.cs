using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDSample1.Users
{
    public class CreatingUserDto
    {
        public String Username { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }

        public CreatingUserDto(String username, String role, String email)
        {
            this.Username = username;
            this.Role = role;
            this.Email = email;
        }

    }
}