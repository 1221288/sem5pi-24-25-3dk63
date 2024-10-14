using DDDSample1.Domain.Shared;

public class LicenseNumber : ValueObject
{
    public string Value { get; private set; }

    public LicenseNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("License number cannot be empty");
        
        Value = value;
    }

        protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}