using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

namespace MediatrExample.API.Logging
{
    public static class MyLogger
    {
        public static void ConfigureSeriLog(IConfiguration configuration)
        {
            string elasticUser = configuration["SeriLogConfig:ElasticUser"];
            string elasticPassword = configuration["SeriLogConfig:ElasticPassword"];
            string environment = configuration["SeriLogConfig:Environment"];
            string projectName = configuration["SeriLogConfig:ProjectName"];
            string elasticUrl = configuration["SeriLogConfig:ElasticUri"];

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
                {
                    AutoRegisterTemplate = true,
                    OverwriteTemplate = true,
                    DetectElasticsearchVersion = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = $"{projectName}-{environment}-logs-" + "{0:yyyy.MM.dd}",
                    //ModifyConnectionSettings = s => s.BasicAuthentication(elasticUser, elasticPassword),
                    FailureCallback = e => { Console.WriteLine("Hasan - Unable to submit event -- " + e.MessageTemplate); },
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                   EmitEventFailureHandling.WriteToFailureSink |
                                   EmitEventFailureHandling.RaiseCallback |
                                   EmitEventFailureHandling.ThrowException,
                    //FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null)
                })
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration, "SerilogLogLevels")
                .CreateLogger();
        }
    }
}
