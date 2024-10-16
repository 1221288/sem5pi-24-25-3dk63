using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.SurgeryRooms.ValueObjects
{
  public class MaintenanceSlot : ValueObject
  {
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public MaintenanceSlot(DateTime startTime, DateTime endTime)
    {
      StartTime = startTime;
      EndTime = endTime;
    }

    public bool ConflictsWith(DateTime otherStartTime, DateTime otherEndTime)
    {
      return StartTime < otherEndTime && EndTime > otherStartTime;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return StartTime;
      yield return EndTime;
    }
  }
}
