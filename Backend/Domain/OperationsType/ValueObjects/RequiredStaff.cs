using System;
using DDDSample1.Domain.Shared;

//NÃO ESTÁ BEM IMPLEMENTADO
namespace DDDSample1.Domain.OperationsType
{
    public class RequiredStaff : IValueObject
    {
        public int Value { get; private set; }

        // Construtor privado para garantir que a classe não é criada diretamente sem validação
        private RequiredStaff() { }

        // Construtor público que valida o número de funcionários necessários
        public RequiredStaff(int value)
        {
            if (value < 0)
                throw new BusinessRuleValidationException("Required staff cannot be negative");

            this.Value = value;
        }

        // Método para mudar o número de funcionários necessários com validação
        public void ChangeValue(int newValue)
        {
            if (newValue < 0)
                throw new BusinessRuleValidationException("Required staff cannot be negative");

            this.Value = newValue;
        }

        // Override do método ToString para facilitar a leitura
        public override string ToString()
        {
            return $"Required Staff: {Value}";
        }

        // Método para verificar igualdade entre instâncias de RequiredStaff
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            RequiredStaff other = (RequiredStaff)obj;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
