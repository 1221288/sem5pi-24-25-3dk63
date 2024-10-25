using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationsType
{
    // Classe que representa uma especialização e a quantidade de funcionários necessários
    public class RequiredStaff : IValueObject
    {
        public int RequiredNumber { get; private set; }

        public RequiredStaff(int requiredNumber)
        {
            if (requiredNumber < 0)
            {
                throw new BusinessRuleValidationException("Required number must be greater than or equal to 0.");
            }

            this.RequiredNumber = requiredNumber;
        }

    }
}
