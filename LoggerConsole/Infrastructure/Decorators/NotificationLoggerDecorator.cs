using LoggerConsole.Contracts;
using LoggerConsole.Domain;
using System;
using System.Collections.Generic;

namespace LoggerConsole.Infrastructure.Decorators
{
    /// <summary>
    /// Decorator que agrega notificaciones simuladas sin modificar la implementación de la clase Logger del cliente.
    /// </summary>
    public class NotificationLoggerDecorator : ILogger
    {
        private readonly ILogger _inner;

        public NotificationLoggerDecorator(ILogger inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public List<Log> GetAll()
        {
            return _inner.GetAll();
        }

        public void Store(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            NotifyIfNeeded(log.Message);
            _inner.Store(log);
        }

        private static void NotifyIfNeeded(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (message.IndexOf("FatalError", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine("[Notificación] Enviando correo a soporteNivel1@email.com y soporteNivel2@email.com");
            }
            else if (message.IndexOf("CriticalError", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine("[Notificación] Enviando correo a soporteNivel1@email.com");
            }
        }
    }
}
