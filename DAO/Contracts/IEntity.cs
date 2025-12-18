using System;

namespace DAO.Contracts
{
    /// <summary>
    /// Entidad base mínima para soportar el repositorio genérico.
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
