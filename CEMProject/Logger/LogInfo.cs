using DataAccessLayer.DbHelper;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace AllAboutLogging
{
    public class LogInfo
    {



        public static void InitializeLoggers(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Logger(logger => ApplySystemLog(logger, configuration))
                .AuditTo.Logger(logger => ApplySystemLogForContact(logger, configuration))
                .AuditTo.Logger(logger => ApplySystemLogForEvent(logger, configuration))
                .CreateLogger();

        }

        private static void ApplySystemLog(LoggerConfiguration logger, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("databaseconn");
            LogEventLevel logLevelSystemLog = GetLogEventLevelFromString(configuration["Serilog:LevelSwitches:$systemLogSwitch"]);


            var sinkOptions = new MSSqlServerSinkOptions { TableName = "Log", AutoCreateSqlTable = true, SchemaName = "logging" };


            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("Email",SqlDbType.NVarChar),

                }
            };

            columnOptions.Store.Remove(StandardColumn.Properties);

            logger
                
                .Filter.ByExcluding(Matching.WithProperty<string>("Logging", (logtype) => { return Loggingtype.AuditAllLog.Contains(logtype); }))
                .WriteTo.MSSqlServer(connectionString, sinkOptions: sinkOptions, restrictedToMinimumLevel: logLevelSystemLog, columnOptions: columnOptions);

        }
        private static void ApplySystemLogForContact(LoggerConfiguration logger, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("databaseconn");
            LogEventLevel logLevelSystemLog = GetLogEventLevelFromString(configuration["Serilog:LevelSwitches:$systemLogSwitch"]);



            var sinkOptions = new MSSqlServerSinkOptions { TableName = "Contact", AutoCreateSqlTable = true, SchemaName = "logging" };

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {

                    new SqlColumn("ContactId", SqlDbType.Int),
                    new SqlColumn("Previous", SqlDbType.NVarChar),
                    new SqlColumn("Current", SqlDbType.NVarChar)

                }
            };


            columnOptions.Store.Remove(StandardColumn.Properties);

            logger
                .Filter.ByIncludingOnly(Matching.WithProperty("Loggingtype", Loggingtype.Contact))

                .AuditTo.MSSqlServer(connectionString, sinkOptions: sinkOptions, restrictedToMinimumLevel: logLevelSystemLog, columnOptions: columnOptions);

        }


        private static void ApplySystemLogForEvent(LoggerConfiguration logger, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("databaseconn");
            LogEventLevel logLevelSystemLog = GetLogEventLevelFromString(configuration["Serilog:LevelSwitches:$systemLogSwitch"]);



            var sinkOptions = new MSSqlServerSinkOptions { TableName = "Event", AutoCreateSqlTable = true, SchemaName = "logging" };

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {

                    new SqlColumn("EventId" , SqlDbType.Int),
                    new SqlColumn("Previous",SqlDbType.NVarChar),
                    new SqlColumn("Current",SqlDbType.NVarChar)


                }
            };

            columnOptions.Store.Remove(StandardColumn.Properties);

            logger

                .Filter.ByIncludingOnly(Matching.WithProperty("Loggingtype", Loggingtype.Event))

                .AuditTo.MSSqlServer(connectionString, sinkOptions: sinkOptions, restrictedToMinimumLevel: logLevelSystemLog, columnOptions: columnOptions);

        }
        private static LogEventLevel GetLogEventLevelFromString(string? logLevelEvent)
        {
            LogEventLevel logEventLevel;
            Enum.TryParse<LogEventLevel>(logLevelEvent, true, out logEventLevel);
            return logEventLevel;
        }

    }
}
