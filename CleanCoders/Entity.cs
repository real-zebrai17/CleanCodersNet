using System;

namespace CleanCoders
{
    public class Entity : ICloneable
    {
        public string Id { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool IsSame(Entity entity)
        {
            return Id != null && Object.Equals(Id, entity.Id);
        }
    }
}