using LoggerConsole.Domain;
using System.Collections.Generic;

namespace LoggerConsole.Contracts
{
    public interface ILogger
    {
        void Store(Log log);

        List<Log> GetAll();
    }
}
