using System;

namespace CleanCoders
{
    public class Entity
    {
        public string Id { get; set; }

        public bool IsSame(Entity entity)
        {
            return Id != null && Object.Equals(Id, entity.Id);
        }
    }
}