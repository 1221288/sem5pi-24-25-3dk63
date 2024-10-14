using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Shared;
namespace Domain.Staff.ValueObjects
{
    public class AvailabilitySlots : ValueObject
    {
        private readonly List<AvailabilitySlot> _slots;

        public AvailabilitySlots()
        {
            _slots = new List<AvailabilitySlot>();
        }

        public void AddSlot(DateTime start, DateTime end)
        {
            var newSlot = new AvailabilitySlot(start, end);
            if (_slots.Any(slot => slot.OverlapsWith(newSlot)))
                throw new ArgumentException("New slot overlaps with another slot.");

            _slots.Add(newSlot);
        }

        public IEnumerable<AvailabilitySlot> Slots => _slots;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var slot in _slots)
            {
                yield return slot.Start;
                yield return slot.End;
            }
        }

        public override string ToString()
        {
            return string.Join("; ", _slots.Select(slot =>
                slot.Start.Date == slot.End.Date
                    ? $"{slot.Start:yyyy-MM-dd:HHhmm}-{slot.End:HHhmm}"
                    : $"{slot.Start:yyyy-MM-dd:HHhmm}/{slot.End:yyyy-MM-dd:HHhmm}"
                ));
        }
    }

    public class AvailabilitySlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public AvailabilitySlot(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("Finishing time must be after start time.");

            Start = start;
            End = end;
        }

        public bool OverlapsWith(AvailabilitySlot other)
        {
            return Start < other.End && End > other.Start;
        }
    }
}