using DataAccessLayer.Models;
using Serilog.Context;
using System.Security.Claims;

namespace APIs.Logger
{
    public class EventLogMiiddleware
    {
        private readonly RequestDelegate next;
        //private readonly CemContext _context;

        public EventLogMiiddleware(RequestDelegate next)
        {
            this.next = next;
            //_context = cemContext;
        }

        public async Task Invoke(HttpContext context)
        {

            
            
           



            await next(context);
        }

    }
}
