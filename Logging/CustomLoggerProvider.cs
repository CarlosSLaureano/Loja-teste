using System.Collections.Concurrent;

namespace LojaAPI.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration loggerConfig;
        readonly ConcurrentDictionary<string, CustomerLogger> loggers = 
                                       new ConcurrentDictionary<string, CustomerLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            loggerConfig = config;
            
        }

        public ILogger CreateLogger(string produtoNome) //observar se vai dar erro, se der trocar por categoryname
        {
            return loggers.GetOrAdd(produtoNome, nome => new CustomerLogger(nome, loggerConfig));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
