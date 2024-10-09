using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain
{

        public class User : Entity<UserId>,IAggregateRoot{

            public string Username { get; private set; }
            public string Role { get; private set; }
            public string Email { get; private set; }

            public bool Active { get; private set; }


            private User() {
                this.Active = true;
             }


            public User(string username, string role, string email) {
                this.Id = new UserId(Guid.NewGuid());
                this.Role = role;
                this.Username = username;
                this.Email = email;
            }


            public void ChangeRole(string role) {
                if(!this.Active) throw new BusinessRuleValidationException("User cannot be changed in this state");
                this.Role = role;
            }


            public void ChangeUsername(string username) {
                if(!this.Active) throw new BusinessRuleValidationException("User cannot be changed in this state");
                this.Username = username;
            }

            public void ChangeEmail(string email) {
                if(!this.Active) throw new BusinessRuleValidationException("User cannot be changed in this state");
                this.Email = email;
            }


        }
}