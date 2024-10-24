using DDDSample1.Domain.Shared;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Staff
{
    public class LicenseNumber : EntityId
    {
        private static readonly Regex LicenseNumberPattern = new Regex(@"^[A-Za-z0-9\-]+$", RegexOptions.Compiled);

        private readonly string Number;

        public LicenseNumber(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("License number cannot be null or empty.");
            }

            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid license number format.");
            }

            this.Number = value;
        }

        [JsonConstructor]
        public LicenseNumber(string value, bool skipValidation) : base(value)
        {
            this.Number = value;
        }

        public static bool IsValid(string licenseNumber)
        {
            return LicenseNumberPattern.IsMatch(licenseNumber);
        }

        protected override object createFromString(string text)
        {
            if (!IsValid(text))
            {
                throw new ArgumentException("Invalid license number format.");
            }
            return text;
        }

        public override string AsString()
        {
            return this.Number;
        }
    }
}
