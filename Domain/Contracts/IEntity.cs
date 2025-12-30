using System;

namespace Domain.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
