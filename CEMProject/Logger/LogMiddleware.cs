using DataAccessLayer.DbHelper;
using Serilog.Context;
using System.Security.Claims;

namespace AllAboutLogging
{
    public class LogMiddleware
    {
        private readonly RequestDelegate next;
        //private readonly ILogger logger;

        public LogMiddleware(RequestDelegate next )
        {
            this.next = next;
            //this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("Loggingtype", Loggingtype.AuditAllLog);
            //var name = context.User.Identity?.Name ?? "system";
            var email = context.User?.FindFirstValue(ClaimTypes.Email)??"system";


            LogContext.PushProperty("Email", email);






            await next(context);
         }

    }
}
