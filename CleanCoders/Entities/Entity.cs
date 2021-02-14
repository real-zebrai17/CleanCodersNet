using System;

namespace CleanCoders.Entities
{
    public class Entity : ICloneable
    {
        public string Id { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool IsSame(Entity entity)
        {
            return Id != null && Equals(Id, entity.Id);
        }
    }
}