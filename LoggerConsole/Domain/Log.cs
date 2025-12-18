using System;

namespace LoggerConsole.Domain
{
    public class Log
    {
        public Log(string message, Severity severity, DateTime? createdAt = null)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }

        public string Message { get; }

        public Severity Severity { get; }

        public DateTime CreatedAt { get; }

        public override string ToString()
        {
            return $"[{CreatedAt:O}] {Severity}: {Message}";
        }
    }
}
