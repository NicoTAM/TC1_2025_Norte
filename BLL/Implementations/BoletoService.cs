using BLL.Contracts;
using DAO.Contracts;
using DAO.Factory;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace BLL.Implementations
{
    public class BoletoService : IBoletoService
    {
        private readonly IBoletoRepository _repository;

        public BoletoService()
            : this(RepositoryFactory.CreateBoletoRepository())
        {
        }

        public BoletoService(IBoletoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IReadOnlyCollection<Boleto> ObtenerTodos()
        {
            return _repository.GetAll();
        }

        public void Registrar(Boleto boleto)
        {
            if (boleto == null)
            {
                throw new ArgumentNullException(nameof(boleto));
            }

            _repository.Insert(boleto);
        }
    }
}
