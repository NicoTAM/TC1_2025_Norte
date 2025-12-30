using DAO.Contracts;
using DAO.Implementations.Memory;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace DAO.Implementations
{
    public class BoletoRepository : IBoletoRepository
    {
        private readonly IGenericRepository<Boleto> _innerRepository;

        public BoletoRepository()
            : this(new InMemoryRepository<Boleto>())
        {
        }

        public BoletoRepository(IGenericRepository<Boleto> innerRepository)
        {
            _innerRepository = innerRepository ?? throw new ArgumentNullException(nameof(innerRepository));
        }

        public void Delete(Guid id)
        {
            _innerRepository.Delete(id);
        }

        public bool Exists(Guid id)
        {
            return _innerRepository.Exists(id);
        }

        public List<Boleto> GetAll()
        {
            return _innerRepository.GetAll();
        }

        public Boleto GetById(Guid id)
        {
            return _innerRepository.GetById(id);
        }

        public void Insert(Boleto entity)
        {
            _innerRepository.Insert(entity);
        }

        public void Update(Boleto entity)
        {
            _innerRepository.Update(entity);
        }
    }
}
