using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCoders.Specs.TestDoubles
{
    public class GatewayUtilities<T>
        where T : Entity
    {
        private readonly Dictionary<string, T> _entities;

        protected IQueryable<T> Entities
        {
            get
            {
                return _entities.Select((kv) => (T)kv.Value)
                                .AsQueryable<T>();
            }
        }

        public GatewayUtilities()
        {
            _entities = new Dictionary<string, T>();
        }

        public T Save(T entity)
        {
            var clone = EstablishId((T)entity.Clone());

            _entities.Add(clone.Id, (clone));
            return clone;
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity.Id);
        }

        private T EstablishId(T entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
                entity.Id = Guid.NewGuid().ToString();
            return entity;
        }
    }
}
