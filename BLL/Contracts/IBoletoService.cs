using Domain.Models;
using System.Collections.Generic;

namespace BLL.Contracts
{
    public interface IBoletoService
    {
        void Registrar(Boleto boleto);

        IReadOnlyCollection<Boleto> ObtenerTodos();
    }
}
