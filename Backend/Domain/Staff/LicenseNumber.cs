using DDDSample1.Domain.Shared;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace DDDSample1.Domain.Staff
{
    public class LicenseNumber : EntityId
    {
        private static readonly Regex LicenseNumberPattern = new Regex(@"^[A-Za-z0-9\-]+$", RegexOptions.Compiled);

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
        }

        [JsonConstructor]
        public LicenseNumber(Guid value) : base(value.ToString())
        {
        }

        public static bool IsValid(string licenseNumber)
        {
            return LicenseNumberPattern.IsMatch(licenseNumber);
        }

        protected override object createFromString(string text)
        {
            return text;
        }

        public override string AsString()
        {
            return (string)base.ObjValue;
        }
        
    }
}
