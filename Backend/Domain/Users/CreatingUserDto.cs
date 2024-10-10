using System;
using DDDSample1.Domain.Users;

namespace DDDSample1.Users
{
    public class CreatingUserDto
    {
        public Role Role { get; set; }
        public Email Email { get; set; }

        public CreatingUserDto(Role role, Email email)
        {
            this.Role = role;
            this.Email = email;
        }
    }
}
