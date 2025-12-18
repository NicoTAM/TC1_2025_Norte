using System;
using System.Collections.Generic;

namespace DAO.Contracts
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        ///Pensamos un CRUD o ABM para cualquier entidad
        ///

        List<T> GetAll();

        T GetById(Guid id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(Guid id);

        bool Exists(Guid id);
    }
}
