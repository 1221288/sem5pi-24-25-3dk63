using DDDSample1.Domain.Shared;
using System.Collections.Generic;

namespace DDDSample1.Domain.OperationRequests
{
    public enum PriorityType
    {
        Elective,
        Urgent,
        Emergency
    }

    public class Priority : ValueObject
    {
        public PriorityType Value { get; private set; }

        public Priority(PriorityType value)
        {
            this.Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }

    public static class Prioritys
    {
        public static Priority Admin => new Priority(PriorityType.Elective);
        public static Priority Doctor => new Priority(PriorityType.Emergency);
        public static Priority Nurse => new Priority(PriorityType.Urgent);
    }
}
