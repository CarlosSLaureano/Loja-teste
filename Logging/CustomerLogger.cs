
namespace LojaAPI.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerNome;

        readonly CustomLoggerProviderConfiguration loggerConfig;
        public CustomerLogger(string nome, CustomLoggerProviderConfiguration config)
        {
            loggerNome = nome;
            loggerConfig = config;
        }


        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"{logLevel.ToString()} : {eventId} - {formatter(state, exception)}";
            EscreverTextoNoArquivo(message);
        }

        private void EscreverTextoNoArquivo(string message)
        {
            string caminhoArquivoLog = @"c:Carlos_Log.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
