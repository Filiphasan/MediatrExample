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

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["SeriLogConfig:ElasticUri"]))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = $"{configuration["SeriLogConfig:ProjectName"]}-{configuration["SeriLogConfig:Environment"]}-logs-{0:yyyy.MM.dd}",
                    ModifyConnectionSettings = s => s.BasicAuthentication(elasticUser, elasticPassword),
                    FailureCallback = e => { Console.WriteLine("Hasan - Unable to submit event -- " + e.MessageTemplate); e.Properties.Values.ToList().ForEach(x => Console.Write(x)); },
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                   EmitEventFailureHandling.WriteToFailureSink |
                                   EmitEventFailureHandling.RaiseCallback |
                                   EmitEventFailureHandling.ThrowException,
                    FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null)
                })
                .Enrich.WithProperty("Environment", configuration["SeriLogConfig:Environment"])
                .ReadFrom.Configuration(configuration, "SerilogLogLevels")
                .CreateLogger();
        }
    }
}
