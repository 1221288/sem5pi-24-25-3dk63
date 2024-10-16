using DDDSample1.Domain.Shared;

namespace Backend.Domain.SurgeryRoom
{
  public class Equipment : ValueObject
  {
    public string Name { get; private set; }
    public string Type { get; private set; }
    public bool IsOperational { get; private set; }

    public Equipment( string name, string type, bool isOperational)
    {
      this.Name = name ;
      this.Type = type ;
      this.IsOperational = isOperational;
    }

    public void SetOperationalStatus(bool status)
    {
      this.IsOperational = status;
    }

    public void ChangeName(string newName)
    {
      if (string.IsNullOrWhiteSpace(newName))
      {
        throw new ArgumentException("Name cannot be empty or null.");
      }
      this.Name = newName;
    }

    public void ChangeType(string newType)
    {
      if (string.IsNullOrWhiteSpace(newType))
      {
        throw new ArgumentException("Type cannot be empty or null.");
      }
      this.Type = newType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return Name;
      yield return Type;
      yield return IsOperational;
    }
  }
}
