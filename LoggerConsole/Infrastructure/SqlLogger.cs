using LoggerConsole.Contracts;
using LoggerConsole.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoggerConsole.Infrastructure
{
    /// <summary>
    /// Implementación simplificada que simula persistencia SQL escribiendo en un archivo plano
    /// que representa la tabla de logs indicada por el connection string.
    /// </summary>
    public class SqlLogger : ILogger
    {
        private readonly string _connectionStringPath;

        public SqlLogger(string connectionStringPath)
        {
            _connectionStringPath = connectionStringPath ?? throw new ArgumentNullException(nameof(connectionStringPath));
        }

        public List<Log> GetAll()
        {
            if (!File.Exists(_connectionStringPath))
            {
                return new List<Log>();
            }

            return File.ReadAllLines(_connectionStringPath)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
                       .Select(ParseLine)
                       .Where(l => l != null)
                       .ToList();
        }

        public void Store(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            string serialized = $"{log.CreatedAt:O};{log.Severity};{log.Message}";
            File.AppendAllLines(_connectionStringPath, new[] { serialized });
        }

        private static Log ParseLine(string line)
        {
            string[] parts = line.Split(new[] { ';' }, 3);
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
                return null;
            }

            return new Log(parts[2], severity, createdAt);
        }
    }
}
