using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;


namespace DDDSample1.Domain.Patients
{
    public class MedicalRecordNumber : EntityId
    {
        // Constructor
        public MedicalRecordNumber(string value) : base(value)
        {
        }

        protected override object createFromString(string text)
        {
            if (!IsValid(text))
            {
                throw new ArgumentException("Invalid medical record number format.");
            }
            return text;
        }

        public override string AsString()
        {
            return (string)this.ObjValue;
        }

        // Validate if the input string is in the correct format
        private static bool IsValid(string entityId)
        {
            if (string.IsNullOrEmpty(entityId) || entityId.Length != 12)
                return false;

            if (!int.TryParse(entityId.Substring(0, 6), out _))
                return false;

            int month = int.Parse(entityId.Substring(4, 2));
            if (month < 1 || month > 12)
                return false;

            return true;
        }

        public static string GenerateNewRecordNumber(DateTime registrationDate, int sequentialNumber)
        {
            string year = registrationDate.ToString("yyyy");
            string month = registrationDate.ToString("MM");
            string seqNum = sequentialNumber.ToString("D6");
            return year + month + seqNum;
        }
    }
}
