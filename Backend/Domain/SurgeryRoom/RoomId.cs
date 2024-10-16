using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace Backend.Domain.SurgeryRoom
{
    public class RoomId : EntityId
    {
        [JsonConstructor]
        public RoomId(Guid value) : base(value)
        {
        }

        
        public RoomId(string value) : base(value)
        {
        }


        override
        protected Object createFromString(String text)
        {
            return new Guid(text);
        }

        override
        public String AsString()
        {
            Guid obj = (Guid)base.ObjValue;
            return obj.ToString();
        }


        public Guid AsGuid()
        {
            return (Guid)base.ObjValue;
        }
    }
}
