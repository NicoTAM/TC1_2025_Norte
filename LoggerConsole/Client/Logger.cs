using System.Collections.Generic;
using System.IO;

namespace LoggerConsole.Client
{
    /// <summary>
    /// Clase proporcionada por el cliente. No debe modificarse.
    /// </summary>
    public class Logger
    {
        private readonly PersistenceType _persistenceType;
        private readonly string _target;

        public Logger(PersistenceType persistenceType, string target)
        {
            _persistenceType = persistenceType;
            _target = target;
        }

        public void Write(string message)
        {
            if (_persistenceType == PersistenceType.File)
            {
                File.AppendAllLines(_target, new[] { message });
            }
            else
            {
                // Simulamos la escritura SQL con un archivo separado que representa la tabla.
                File.AppendAllLines(_target, new[] { message });
            }
        }

        public string[] ReadAll()
        {
            if (!File.Exists(_target))
            {
                return new string[0];
            }

            return File.ReadAllLines(_target);
        }
    }
}
