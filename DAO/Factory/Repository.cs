using DAO.Contracts;
using DAO.Factory.Enums;
using DAO.Implementations.Memory;
using System;
using System.Configuration;

namespace DAO.Factory
{
    /// <summary>
    /// Factory responsable de entregar la implementación concreta de los repositorios.
    /// </summary>
    public static class RepositoryFactory
    {
        private static readonly BackendType Backend = ResolveBackendType();

        public static IGenericRepository<T> Create<T>() where T : class, IEntity
        {
            switch (Backend)
            {
                case BackendType.Memory:
                    return new InMemoryRepository<T>();
                default:
                    throw new NotSupportedException($"El backend {Backend} no está soportado en el proyecto base.");
            }
        }

        private static BackendType ResolveBackendType()
        {
            string backendSetting = ConfigurationManager.AppSettings["BackendType"];

            if (Enum.TryParse(backendSetting, ignoreCase: true, out BackendType parsed))
            {
                return parsed;
            }

            return BackendType.Memory;
        }
    }
}
