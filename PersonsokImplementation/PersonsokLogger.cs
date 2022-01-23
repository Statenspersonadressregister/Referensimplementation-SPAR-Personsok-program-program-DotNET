using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace PersonsokImplementation
{
    /// <summary>
    /// En klass för att logga till konsol och till loggfil.
    /// </summary>
    public class PersonsokLogger
    {
        private readonly ILogger<PersonsokLogger> _logger;

        public PersonsokLogger(ILogger<PersonsokLogger> logger)
        {
            _logger = logger;
        }

        public void LogCritical(string message)
        {
            _logger.LogCritical(message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        /// <summary>
        /// Skapar och returnerar en logger,
        /// om det redan har skapats en logger tidigare så returneras den existerande.
        /// </summary>
        /// <returns>PersonsokLogger</returns>
        public static PersonsokLogger CreatePersonsokLogger()
        {
            LoggingConfiguration logConfig = new NLog.Config.LoggingConfiguration();
            FileTarget logfile = new NLog.Targets.FileTarget("logfile") { FileName = "personsok.log" };
            ConsoleTarget logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            logConfig.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            logConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);
            NLog.LogManager.Configuration = logConfig;

            IConfigurationRoot rootConfig = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<PersonsokLogger>()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog(rootConfig);
                })
                .BuildServiceProvider();

            return serviceProvider.GetRequiredService<PersonsokLogger>();
        }
    }
}