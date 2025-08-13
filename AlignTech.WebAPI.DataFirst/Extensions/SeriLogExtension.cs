using Serilog;

namespace AlignTech.WebAPI.DataFirst.Extensions
{
    public static class SeriLogExtension
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            //Register Serilog Configuration
            Log.Logger = new LoggerConfiguration()
                // Add enrichers (additional context to every log entry)
                .Enrich.FromLogContext()        // Allows using LogContext.PushProperty()
                .Enrich.WithMachineName()       // Add Machine Name
                .Enrich.WithThreadId()          // Adds thread ID
                .Enrich.WithEnvironmentName()   // Adds environment (Dev/Prod)

                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Async(wt => wt.File("Logs/log-.txt", rollingInterval: RollingInterval.Day))
                .WriteTo.Async(wt => wt.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("MyConn"),
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "LogEvents",
                        AutoCreateSqlTable = true
                    }))
                .CreateLogger();

            builder.Host.UseSerilog();

        }
    }
}
