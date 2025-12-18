using LoggerConsole.Contracts;
using LoggerConsole.Domain;
using LoggerConsole.Infrastructure.Factories;
using LoggerConsole.Settings;
using System;

namespace LoggerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ApplicationSettings settings = ApplicationSettings.Load();
            ILogger logger = LoggerFactory.Create(settings);

            Console.WriteLine("Registrando logs de ejemplo...\n");

            logger.Store(new Log("Proceso completado correctamente", Severity.Info));
            logger.Store(new Log("CriticalError: falta de memoria", Severity.Critical));
            logger.Store(new Log("FatalError: caída general del sistema", Severity.Fatal));

            Console.WriteLine("\nLectura de logs persistidos:");

            foreach (var log in logger.GetAll())
            {
                Console.WriteLine(log.ToString());
            }

            Console.WriteLine("\nPresione cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}
