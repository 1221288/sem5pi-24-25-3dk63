using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using Microsoft.Extensions.Configuration;

namespace DDDSample1.Domain
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public Username Username { get; private set; }
        public Role Role { get; private set; }
        public Email Email { get; private set; }
        public bool Active { get; private set; }

        public int SequentialNumber { get; private set; }


        private User()
        {
            this.Active = true;
        }

        public User(Role role, Email email, int recruitmentYear,string domain, int sequentialNumber)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Active = true;
            this.Email = email;
            this.Role = role;
            this.SequentialNumber = sequentialNumber;
            this.Username = GenerateUsername(role.Value, recruitmentYear,domain,sequentialNumber);
        }

        private Username GenerateUsername(RoleType roleType, int recruitmentYear,string domain,int sequentialNumber)
        {
            string prefix;

            switch (roleType)
            {
                case RoleType.Doctor:
                    prefix = "D";
                    break;
                case RoleType.Nurse:
                    prefix = "N";
                    break;
                case RoleType.Technician:
                case RoleType.Admin:
                case RoleType.Patient:
                    prefix = "O";
                    break;
                default:
                    throw new ArgumentException("RoleType inválido.");
            }

            // Formato: (Prefixo)AnoSequencial -> Ex: D202400001
            string username = $"{prefix}{recruitmentYear}{sequentialNumber:D5}@{domain}"; // Adiciona o domínio

            return new Username(username);
        }

        public void ChangeRole(Role role)
        {
            if (!this.Active) throw new BusinessRuleValidationException("User cannot be changed in this state");
            this.Role = role;
        }

        public void ChangeUsername(Username username)
        {
            if (!this.Active) throw new BusinessRuleValidationException("User cannot be changed in this state");
            this.Username = username;
        }

        public void ChangeEmail(Email email)
        {
            if (!this.Active) throw new BusinessRuleValidationException("User cannot be changed in this state");
            this.Email = email;
        }
    }
}