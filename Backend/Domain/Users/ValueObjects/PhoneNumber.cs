using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace Backend.Domain.Users.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; private set; }

        public PhoneNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number cannot be empty.");

            if (!IsValidPhoneNumber(number))
                throw new ArgumentException("Invalid phone number format.");

            this.Number = number;
        }

        private bool IsValidPhoneNumber(string number)
        {
            // Verificar se começa com '+' e se o resto contém apenas dígitos
            if (!number.StartsWith("+") || number.Length < 10 || number.Length > 15)
                return false;

            // Verificar se os caracteres depois do '+' são todos dígitos
            return long.TryParse(number.Substring(1), out _);
        }


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(PhoneNumber))
                return false;

            PhoneNumber other = (PhoneNumber)obj;
            return this.Number == other.Number;
        }

        public override int GetHashCode()
        {
            return this.Number.GetHashCode();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
