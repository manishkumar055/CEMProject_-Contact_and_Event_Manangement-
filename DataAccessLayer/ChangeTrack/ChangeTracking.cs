using DataAccessLayer.DbHelper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccessLayer.ChangeTrack
{
    public class ChangeTracking : IChangeTracking
    {
        private readonly CemContext _context;
        private readonly ILogger<ChangeTracking> _logger;

        public ChangeTracking(CemContext context, ILogger<ChangeTracking> logger)
        {
            _context = context;
            _logger = logger;

        }

        public void ContactChangesPush()
        {
            try

            {



                var entries = _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged && x.State != EntityState.Detached).ToList();

                var options = new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve };
                foreach(var entry in entries) 
                {
                    string jsonString = JsonSerializer.Serialize(entry. OriginalValues.ToObject(), options);
                    string currentObj=JsonSerializer.Serialize(entry.CurrentValues.ToObject(), options);

                   

                    LogLevel level = LogLevel.Information;
                    var messageTemplate = "{Entity} Was {State}";
                    using (LogContext.PushProperty("Loggingtype", Loggingtype.Contact))
                    //using (LogContext.PushProperty("ContactId", entry.curr))
                    using (LogContext.PushProperty("Previous", jsonString))
                    using (LogContext.PushProperty("Current", currentObj))
                        _logger.Log(level, messageTemplate, entry.Metadata.GetTableName(), entry.State.ToString());
                };

            }

            catch (Exception)
            {

                throw;
            }
        }


        public void EventChangesPush()
        {
            try

            {

                var entries = _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged && x.State != EntityState.Detached).ToList();

                var options = new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve };
                foreach (var entry in entries)
                {
                    string jsonString = JsonSerializer.Serialize(entry.OriginalValues.ToObject(), options);
                    string currentObj = JsonSerializer.Serialize(entry.CurrentValues.ToObject(), options);

                    LogLevel level = LogLevel.Information;
                    var messageTemplate = "{Entity} Was {State}";
                    using (LogContext.PushProperty("Loggingtype", Loggingtype.Event))
                    //using (LogContext.PushProperty("EventId", entry.CurrentValues))
                    using (LogContext.PushProperty("Previous", jsonString))
                    using (LogContext.PushProperty("Current", currentObj))
                        _logger.Log(level, messageTemplate, entry.Metadata.GetTableName(), entry.State.ToString());
                };

            }


            catch (Exception)
            {

                throw;
            }
        }


    }
}
