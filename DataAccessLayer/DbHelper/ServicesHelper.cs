using DataAccessLayer.Email;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccessLayer.DbHelper
{
    public class ServicesHelper : IServicesHelper
    {
        private readonly CemContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailInjection _email;

        public ServicesHelper(CemContext context, IHttpContextAccessor httpContextAccessor,IEmailInjection emailInjection)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
            _email = emailInjection;
        }

        public async Task<List<Country>> GetCountries()
        {
            try
            {
                var countries=await _context.Countries.Include(x=>x.States).ToListAsync();
                return countries;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CounterParty> ChekCounterParty(string companyName)
        {
            try
            {
                var cmpObj = await _context.CounterParties.Where(x => x.CompanyName.Equals(companyName)).Include(x => x.Contacts).FirstOrDefaultAsync();
                if (cmpObj == null)
                {
                    var createdBy = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "system";
                    var empId = Convert.ToInt32(_contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                    cmpObj = new CounterParty
                    {
                        CompanyName = companyName,
                        CreatedBy = createdBy,
                        EmployeeId = empId,

                    };
                    await _context.CounterParties.AddAsync(cmpObj);
                    await _context.SaveChangesAsync();
                    var comlianceObj = await _context.Employees.FirstOrDefaultAsync(x => x.Role.Name.Equals("ComplianceMember"));
                    await _email.SendEmailBySendGrid(new EmailDto
                    {
                        To = comlianceObj.Email,
                        Body = $"Please Aprove this CounterParty {companyName}",
                        Message = "Thankyou",

                    });
                }
                return cmpObj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Contact> ChekContact(int contactId)
        {
            var contactObj=await _context.Contacts.Where(x=>x.Id== contactId)
                                                    .Include(x=>x.ContactAddresses)
                                                    .Include(x=>x.ContactInfos).FirstOrDefaultAsync();
            if (contactObj == null) { throw new Exception($"Contact Not Found For This ContactId {contactId}"); }
            return contactObj;
        }
    }                                                                                 
}
