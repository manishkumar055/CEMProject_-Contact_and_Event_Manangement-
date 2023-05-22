using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbHelper
{
    public class Loggingtype
    {
        public const string Contact = "Contact";
        public const string Event = "Event";
        public static string[] AuditAllLog = new[] { Contact,Event}; 

    }
}
