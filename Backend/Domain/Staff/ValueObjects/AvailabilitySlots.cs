using System.Text.Json;

namespace Backend.Domain.Staff.ValueObjects
{
    public class AvailabilitySlots
    {
        public List<AvailabilitySlot> Slots { get; private set; }

        public AvailabilitySlots()
        {
            Slots = new List<AvailabilitySlot>();
        }

        public void AddSlot(DateTime start, DateTime end)
        {
            Slots.Add(new AvailabilitySlot(start, end));
        }

        public string SerializeSlots()
        {
            return JsonSerializer.Serialize(Slots);
        }

        public static AvailabilitySlots DeserializeSlots(string json)
        {
            var slots = JsonSerializer.Deserialize<List<AvailabilitySlot>>(json);
            return new AvailabilitySlots { Slots = slots ?? new List<AvailabilitySlot>() };
        }
    }

    public class AvailabilitySlot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public AvailabilitySlot(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
