using LoggerConsole.Client;
using LoggerConsole.Contracts;
using LoggerConsole.Infrastructure.Adapters;
using LoggerConsole.Infrastructure.Decorators;
using LoggerConsole.Settings;
using System;

namespace LoggerConsole.Infrastructure.Factories
{
    public static class LoggerFactory
    {
        public static ILogger Create(ApplicationSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            ILogger logger = settings.UseClientLogger
                ? CreateClientAdapter(settings)
                : CreateNativeLogger(settings);

            return new NotificationLoggerDecorator(logger);
        }

        private static ILogger CreateNativeLogger(ApplicationSettings settings)
        {
            switch (settings.Persistence)
            {
                case PersistenceType.File:
                    return new Infrastructure.FileLogger(settings.FilePath);
                case PersistenceType.Sql:
                    return new Infrastructure.SqlLogger(settings.SqlConnectionString);
                default:
                    throw new NotSupportedException($"Persistence type {settings.Persistence} is not supported.");
            }
        }

        private static ILogger CreateClientAdapter(ApplicationSettings settings)
        {
            var clientLogger = new Logger(settings.Persistence, settings.Persistence == PersistenceType.File
                ? settings.FilePath
                : settings.SqlConnectionString);

            return new ClientLoggerAdapter(clientLogger);
        }
    }
}
