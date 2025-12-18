using DAO.Contracts;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAO.Implementations.Memory
{
    /// <summary>
    /// Implementación simple en memoria para el repositorio genérico.
    /// </summary>
    /// <typeparam name="T">Entidad de dominio que implementa <see cref="IEntity"/>.</typeparam>
    public sealed class InMemoryRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly Dictionary<Guid, T> _items = new Dictionary<Guid, T>();

        public List<T> GetAll()
        {
            return _items.Values.ToList();
        }

        public T GetById(Guid id)
        {
            return _items.TryGetValue(id, out T value) ? value : null;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            _items[entity.Id] = entity;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!_items.ContainsKey(entity.Id))
            {
                throw new KeyNotFoundException("La entidad no existe en el repositorio.");
            }

            _items[entity.Id] = entity;
        }

        public void Delete(Guid id)
        {
            _items.Remove(id);
        }

        public bool Exists(Guid id)
        {
            return _items.ContainsKey(id);
        }
    }
}
