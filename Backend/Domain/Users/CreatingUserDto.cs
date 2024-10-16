using System;
using DDDSample1.Domain.Users;

namespace DDDSample1.Users
{
    public class CreatingUserDto
    {
        public Role Role { get; set; }
        public Email Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   

        public CreatingUserDto(Role role, Email email, string firstName, string lastName)
        {
            this.Role = role;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
