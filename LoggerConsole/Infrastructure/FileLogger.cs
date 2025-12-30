using LoggerConsole.Contracts;
using LoggerConsole.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoggerConsole.Infrastructure
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public List<Log> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Log>();
            }

            return File.ReadAllLines(_filePath)
                       .Where(l => !string.IsNullOrWhiteSpace(l))
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

            string serialized = $"{log.CreatedAt:O}|{log.Severity}|{log.Message}";
            File.AppendAllLines(_filePath, new[] { serialized });
        }

        private static Log ParseLine(string line)
        {
            string[] parts = line.Split(new[] { '|' }, 3);
            if (parts.Length != 3)
            {
                return null;
            }

            if (!DateTime.TryParse(parts[0], out DateTime timestamp))
            {
                return null;
            }

            if (!Enum.TryParse(parts[1], out Severity severity))
            {
                return null;
            }

            return new Log(parts[2], severity, timestamp);
        }
    }
}
