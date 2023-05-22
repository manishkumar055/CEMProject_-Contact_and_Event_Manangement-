using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Security.Claims;

namespace APIs.Logger
{
    public class ContactLogMiddleware
    {
        private readonly RequestDelegate next;

        public ContactLogMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var name = context.User?.Claims.FirstOrDefault(x => x.Type is ClaimTypes.Email or ClaimTypes.MobilePhone).Value;
            if(name == null)
            {
                name = "system";
            }
            var Id = Convert.ToInt32(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            LogContext.PushProperty("EventId", Id);
            LogContext.PushProperty("Previous", "system");
            LogContext.PushProperty("Current", "CurrentSystem");

            var EventId = Convert.ToInt32(context.User.Claims.FirstOrDefault(X => X.Type == ClaimTypes.NameIdentifier)?.Value);


            LogContext.PushProperty("EventId", EventId);
            LogContext.PushProperty("Previous", "system");
            LogContext.PushProperty("Current", "CurrentSystem");

            await next(context);
        }
    }
}
