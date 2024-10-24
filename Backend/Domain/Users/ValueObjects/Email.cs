using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users
{
    public class Email : ValueObject
    {
        public string Value { get; private set; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessRuleValidationException("Email cannot be empty.");
            }

            if (!IsValidEmail(value))
            {
                throw new BusinessRuleValidationException("Invalid email format.");
            }

            this.Value = value;
        }

        private bool IsValidEmail(string email)
        {
            // Dividir o email em duas partes: local-part e domain-part
            var parts = email.Split('@');

            // Verificar se o email contém exatamente um "@" e que não está vazio
            if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
            {
                return false;
            }

            var localPart = parts[0];
            var domainPart = parts[1];

            // Verificar se o domain-part contém um ponto (.) e que há partes válidas antes e depois do ponto
            var domainParts = domainPart.Split('.');
            if (domainParts.Length < 2 || string.IsNullOrWhiteSpace(domainParts[0]) || string.IsNullOrWhiteSpace(domainParts[^1]))
            {
                return false;
            }

            // Verifica se o TLD (após o último ponto) tem pelo menos dois caracteres
            if (domainParts[^1].Length < 2)
            {
                return false;
            }

            // Verifica se o domínio não contém dois pontos consecutivos
            if (domainPart.Contains(".."))
            {
                return false;
            }

            // Adicionalmente, verifica se o local-part ou domain-part contém caracteres inválidos
            return true;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
