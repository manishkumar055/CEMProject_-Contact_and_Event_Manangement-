using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Email
{
    public interface IEmailInjection
    {
        public  Task<string> SendEmail(EmailDto email);
        public Task<string> SendEmailBySendGrid(EmailDto request);
    }
}
