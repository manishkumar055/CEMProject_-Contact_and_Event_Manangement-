using DataAccessLayer.Models;
using MailKit.Net.Smtp;

using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DataAccessLayer.Email
{
    public class EmailInjection : IEmailInjection
    {
        private readonly CemContext _context;
        private readonly ISendGridClient _sendGridClient;
        private IConfiguration _configuration;
        public EmailInjection(CemContext context,IConfiguration configuration, ISendGridClient sendGridClient)
        {
            _context = context;
            _configuration = configuration; 
            _sendGridClient = sendGridClient;
        }

        public async Task<string> SendEmail(EmailDto request)
        {
            try
            {
                //var eventAprovalObj = await _context.EventApprovals.Where(x => x.ContactId == request.EventAprovalId&& x.IsDeleted==false).Include(x=>x.Event).FirstOrDefaultAsync();
                //if (eventAprovalObj == null)
                //{
                //    throw new Exception($"EventAprovalId Not Found {request.EventAprovalId}");
                //}
                //request.Message += $" Event Title is {eventAprovalObj.Event.Title}, Agenda is {eventAprovalObj.Event.Agenda} ";
               
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Message;
                email.Body=new TextPart(TextFormat.Text) { Text= request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return $"Mail Send to {request.To}";
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> SendEmailBySendGrid(EmailDto request)
        {
            try
            {
                //var eventAprovalObj = await _context.EventApprovals.Where(x => x.ContactId == request.EventAprovalId && x.IsDeleted == false).Include(x => x.Event).FirstOrDefaultAsync();
                //if (eventAprovalObj == null)
                //{
                //    throw new Exception($"EventAprovalId Not Found {request.EventAprovalId}");
                //}
                //request.Message += $" Event Title is {eventAprovalObj.Event.Title}," +
                //    $" Agenda is {eventAprovalObj.Event.Agenda} Budget is {eventAprovalObj.Event.EstimateCost} ";

                var fromEmail = _configuration.GetSection("EmailAdd").Value;
                var fromName = _configuration.GetSection("FromName").Value;
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(fromEmail),
                    Subject = "Request for EventApproval",

                    PlainTextContent = request.Message,
                    //IpPoolName=fromName

                };
                msg.AddTo(request.To);
                var response = await _sendGridClient.SendEmailAsync(msg);
                var res = response.IsSuccessStatusCode ? "Email Send" : "Sending Failed";
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
