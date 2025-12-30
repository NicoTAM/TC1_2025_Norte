using LoggerConsole.Client;
using System;
using System.Configuration;

namespace LoggerConsole.Settings
{
    public class ApplicationSettings
    {
        public PersistenceType Persistence { get; private set; }

        public string FilePath { get; private set; }

        public string SqlConnectionString { get; private set; }

        public bool UseClientLogger { get; private set; }

        public static ApplicationSettings Load()
        {
            var settings = new ApplicationSettings
            {
                Persistence = ParseEnum(ConfigurationManager.AppSettings["PersistenceType"], PersistenceType.File),
                FilePath = ConfigurationManager.AppSettings["LogFilePath"],
                SqlConnectionString = ConfigurationManager.AppSettings["SqlConnectionString"],
                UseClientLogger = ParseBool(ConfigurationManager.AppSettings["UseClientLogger"], defaultValue: true)
            };

            Validate(settings);
            return settings;
        }

        private static void Validate(ApplicationSettings settings)
        {
            if (settings.Persistence == PersistenceType.File && string.IsNullOrWhiteSpace(settings.FilePath))
            {
                throw new ConfigurationErrorsException("LogFilePath is required for file persistence.");
            }

            if (settings.Persistence == PersistenceType.Sql && string.IsNullOrWhiteSpace(settings.SqlConnectionString))
            {
                throw new ConfigurationErrorsException("SqlConnectionString is required for SQL persistence.");
            }
        }

        private static PersistenceType ParseEnum(string value, PersistenceType defaultValue)
        {
            if (Enum.TryParse(value, ignoreCase: true, out PersistenceType parsed))
            {
                return parsed;
            }

            return defaultValue;
        }

        private static bool ParseBool(string value, bool defaultValue)
        {
            if (bool.TryParse(value, out bool parsed))
            {
                return parsed;
            }

            return defaultValue;
        }
    }
}
