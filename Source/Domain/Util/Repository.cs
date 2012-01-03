using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Util
{
    public class Repository
    {
        private IList<Entity> _everything;

        public Repository()
        {
            _everything = new List<Entity>();
        }

        public IEnumerable<T> Query<T>() where T : Entity
        {
            lock(_everything)
                return _everything.Where(e => e is T).Cast<T>().ToList();
        }

        public T Load<T>(int id) where T : Entity
        {
            lock(_everything)
                return (T)_everything.Where(e => e.Id == id).Single();
        }

        public T Save<T>(T entity) where T : Entity
        {
            lock(_everything)
            {
                if (entity.Id != 0)
                    throw new Exception("Entity is already persisted");

                entity.Id = _everything.Select(e => e.Id).DefaultIfEmpty().Max() + 1;
                _everything.Add(entity);
                return entity;
            }
        }
    }
}