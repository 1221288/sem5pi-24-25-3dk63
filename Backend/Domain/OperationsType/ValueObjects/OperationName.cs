using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationsType
{
    public class OperationName : IValueObject
    {
        public string Description { get; private set; }

        public OperationName(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new BusinessRuleValidationException("Description can't be null or empty.");
            }

            this.Description = description;
        }

    }
}
