using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Appointments
{
    public class Date : ValueObject
{
    public DateTime Value { get; private set; }

    public Date(DateTime value)
    {
        if (value < DateTime.Now)
            throw new BusinessRuleValidationException("Date cannot be in the past");

        this.Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
}