using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using NLog.Extensions.Logging;
using TemplateConsoleProject.Options;
using System.Collections.Generic;
using CommandLine;
using TemplateConsoleProject.Language;
using System.Diagnostics;

namespace TemplateConsoleProject
{
    /// <summary>
    /// The main <c>Program</c> class.
    /// Contains all the logic for running the application via the command line
    /// </summary>
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static ILogger _logger;
        private static ILanguageService _languageService;
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(
                    Assembly.GetEntryAssembly().Location))
#if DEBUG
                .AddJsonFile($"appsettings.Development.json", false)
#else
                .AddJsonFile("appsettings.json", false)
#endif
                .Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(loggingBuilder => {
                loggingBuilder.AddNLog("nlog.config");
            });
            var localization = Configuration.GetValue<string>("localization");
            serviceCollection.AddSingleton<ILanguageService>(new LanguageService(localization));
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _logger = _serviceProvider.GetService<ILogger<Program>>();
            _languageService = _serviceProvider.GetService<ILanguageService>();

            Parser.Default.ParseArguments<
                RunOptions>(args)
                .MapResult(
                    (RunOptions options) => Run(options),
                    errs => HandleError(errs));
        }

        /// <summary>
        /// This executes the logic for the Run command
        /// </summary>
        /// <returns>
        /// Returns 1 when completed.
        /// </returns>
        static int Run(RunOptions options)
        {
            _logger.LogInformation(_languageService.GetText("INFO00001"));

            var p = new Process();
            p.StartInfo.FileName = "exportLegacy.exe";
            p.StartInfo.Arguments = " -user " + Console.ReadLine() + " -role user";
            p.Start();
            return 1;
        }

        /// <summary>
        /// This will log all the errors generated from the coammnd line parser
        /// </summary>
        /// <returns>
        /// Returns 1 when completed.
        /// </returns>
        static int HandleError(IEnumerable<Error> errs)
        {
            foreach (var error in errs)
                _logger.LogCritical($"{error}");

            return 1;
        }

    }
}
