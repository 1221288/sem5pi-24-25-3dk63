using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationsType
{
    // Classe que representa uma especialização e a quantidade de funcionários necessários
    public class StaffSpecialization : IValueObject
    {
        public string Specialization { get; private set; }
        public int RequiredNumber { get; private set; }

        public StaffSpecialization(string specialization, int requiredNumber)
        {
            if (string.IsNullOrEmpty(specialization))
                throw new BusinessRuleValidationException("Specialization cannot be null or empty");

            if (requiredNumber < 0)
                throw new BusinessRuleValidationException("Number of required staff cannot be negative");

            Specialization = specialization;
            RequiredNumber = requiredNumber;
        }

        // Override de ToString para facilitar a leitura
        public override string ToString()
        {
            return $"{Specialization}: {RequiredNumber}";
        }

        // Método Equals para verificar igualdade entre instâncias
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            StaffSpecialization other = (StaffSpecialization)obj;
            return Specialization == other.Specialization && RequiredNumber == other.RequiredNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Specialization, RequiredNumber);
        }
    }

    // Classe principal que contém a lista de especializações e número de funcionários por especialização
    public class RequiredStaff : IValueObject
    {
        public List<StaffSpecialization> StaffBySpecialization { get; private set; }

        // Construtor público que valida a lista de especializações
        public RequiredStaff(List<StaffSpecialization> staffBySpecialization)
        {
            if (staffBySpecialization == null || staffBySpecialization.Count == 0)
                throw new BusinessRuleValidationException("There must be at least one staff specialization");

            StaffBySpecialization = new List<StaffSpecialization>(staffBySpecialization);
        }

        // Método para adicionar ou atualizar uma especialização
        public void AddOrUpdateSpecialization(string specialization, int requiredNumber)
        {
            if (requiredNumber < 0)
                throw new BusinessRuleValidationException("Number of required staff cannot be negative");

            var existingSpecialization = StaffBySpecialization.Find(s => s.Specialization == specialization);
            if (existingSpecialization != null)
            {
                // Atualizar a quantidade se já existir a especialização
                StaffBySpecialization.Remove(existingSpecialization);
                StaffBySpecialization.Add(new StaffSpecialization(specialization, requiredNumber));
            }
            else
            {
                // Adicionar uma nova especialização
                StaffBySpecialization.Add(new StaffSpecialization(specialization, requiredNumber));
            }
        }

        // Método para remover uma especialização
        public void RemoveSpecialization(string specialization)
        {
            var existingSpecialization = StaffBySpecialization.Find(s => s.Specialization == specialization);
            if (existingSpecialization != null)
            {
                StaffBySpecialization.Remove(existingSpecialization);
            }
        }

        // Override de ToString para facilitar a leitura
        public override string ToString()
        {
            return string.Join(", ", StaffBySpecialization);
        }

        // Override de Equals e GetHashCode para comparar instâncias
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            RequiredStaff other = (RequiredStaff)obj;
            if (StaffBySpecialization.Count != other.StaffBySpecialization.Count)
                return false;

            for (int i = 0; i < StaffBySpecialization.Count; i++)
            {
                if (!StaffBySpecialization[i].Equals(other.StaffBySpecialization[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StaffBySpecialization);
        }
    }
}
