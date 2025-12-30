using LoggerConsole.Client;
using LoggerConsole.Contracts;
using LoggerConsole.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoggerConsole.Infrastructure.Adapters
{
    /// <summary>
    /// Adapter que permite reutilizar la clase Logger del cliente cumpliendo con ILogger.
    /// </summary>
    public class ClientLoggerAdapter : ILogger
    {
        private readonly Logger _clientLogger;

        public ClientLoggerAdapter(Logger clientLogger)
        {
            _clientLogger = clientLogger ?? throw new ArgumentNullException(nameof(clientLogger));
        }

        public List<Log> GetAll()
        {
            return _clientLogger.ReadAll()
                                .Where(line => !string.IsNullOrWhiteSpace(line))
                                .Select(ParseLine)
                                .Where(log => log != null)
                                .ToList();
        }

        public void Store(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            string payload = $"{log.CreatedAt:O}|{log.Severity}|{log.Message}";
            _clientLogger.Write(payload);
        }

        private static Log ParseLine(string line)
        {
            string[] parts = line.Split(new[] { '|' }, 3);
            if (parts.Length != 3)
            {
                return null;
            }

            if (!DateTime.TryParse(parts[0], out DateTime createdAt))
            {
                return null;
            }

            if (!Enum.TryParse(parts[1], out Severity severity))
            {
                severity = Severity.Info;
            }

            return new Log(parts[2], severity, createdAt);
        }
    }
}
