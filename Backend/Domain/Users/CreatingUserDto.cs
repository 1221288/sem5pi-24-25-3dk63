using System;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Users
{
    public class CreatingUserDto
    {
        public Role Role { get; set; }
        public Email Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PhoneNumber phoneNumber { get; set; }

        public CreatingUserDto(Role role, Email email, string firstName, string lastName, PhoneNumber phoneNumber)
        {
            this.Role = role;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.phoneNumber = phoneNumber;
        }
    }
}
