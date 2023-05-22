using DataAccessLayer.ChangeTrack;
using DataAccessLayer.Email;
using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace DataAccessLayer.DbMethods
{


    public class ContactMethods : IContactMethods
    {
        private readonly CemContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private  string  loggedInUser = "system";
        //private readonly IEmailInjection _email;
        //private readonly IChangeTracking _track;

        //private ILogger _logger;


        public ContactMethods(CemContext context, IHttpContextAccessor httpContextAccessor/* , IEmailInjection email, IChangeTracking changeTracking*/)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            //_email = email;
            //_track = changeTracking;
        }// Constructor

        public async Task<string> AddContact(Contact contact)
        {
            try
            {
                var empId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

                var loggedInEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? loggedInUser;
 
                contact.EmployeeId = empId;
                contact.CreatedBy= loggedInEmail;
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                Log.ForContext("logging.Contact.Current", "Hello");
                await _context.SaveChangesAsync();

                return "Contact Added SuccessFully...";


            }
            catch (Exception)
            {

                throw;
            }

        }// Adding Conatact  and their Addresses and Info's by Employees


        public async Task<string> DeleteContact(int contactId)
        {
            try
            {
                var contactObj = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
                if (contactObj == null)
                {
                    throw new Exception("Contact Not Found");
                }
                var deletedBy = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? loggedInUser;

                contactObj.IsDeleted = true;
                contactObj.DeletedBy = deletedBy;
                await _context.SaveChangesAsync();
                return $"Contact Deleted Of ContactId {contactId}";
            }
            catch (Exception)
            {

                throw;
            }
        }// Deleting Contact by Employee

        public async Task<List<Contact>> GetAllContacts(string companyName)
        {
            var countetrPartyObj = await _context.CounterParties.Where(x => x.CompanyName == companyName&& x.IsDeleted==false).Include(x => x.Contacts).FirstOrDefaultAsync();
            if (countetrPartyObj == null) { throw new Exception($"CouterParty Not Found For Name {companyName}"); }
            return countetrPartyObj.Contacts.Where(x=>x.IsDeleted==false).ToList();

            
        }// Returning All Contacts


        public async Task<string> UpdateContact(Contact request)
        {
            try
            {
                var updatedBy = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
                request.UpdatedBy= updatedBy;

                await _context.SaveChangesAsync();
              


                return "Update SuccessFully Changed...";



            }
            catch (Exception)
            {

                throw;
            }
        }// Upadating Contact Details


    }
}
