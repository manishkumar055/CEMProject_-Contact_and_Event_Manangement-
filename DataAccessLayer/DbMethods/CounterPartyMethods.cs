
using DataAccessLayer.Email;
using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccessLayer.DbMethods
{
    public class CounterPartyMethods : ICounterPartyMethods
    {
        private readonly CemContext _context;
        IEmailInjection _email;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string loggedInUser = "system";
        public CounterPartyMethods(CemContext context, IEmailInjection emailInjection, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _email = emailInjection;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddCounterParty(CounterParty party)
        {
            try
            {
                var createdBy = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? loggedInUser;
                var empId =Convert.ToInt32 (_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var partyObj = await _context.CounterParties
                    .Where(x => x.CompanyName.Equals(party.CompanyName))
                    .FirstOrDefaultAsync();
                if (partyObj != null)
                {
                    throw new Exception($"This Counter Party alrady Added and its approval is {partyObj.IsApproved}");
                }
                var EmpObj = await _context.Employees.FirstOrDefaultAsync(x => x.Role.Name == "ComplianceMember");

                party.CreatedBy = createdBy;
                party.EmployeeId= empId;
                await _context.CounterParties.AddAsync(party);
                await _context.SaveChangesAsync();
                await _email.SendEmailBySendGrid(new EmailDto
                {
                    To = EmpObj.Email,
                    Body = "Hello",
                    Message = $"Please Approve This CounterParty Name is {party.CompanyName} ContactId is {party.Id}"

                });
                return $"CounterParty {party.CompanyName} Added and Sended For Approval to {EmpObj.Name}";

            }
            catch (Exception)
            {

                throw;
            }
        } // Adding New CounterParty By Name

        public async Task<string> DeleteCounterParty(string partyName)
        {
            try
            {
                var CpartyObj = await _context.CounterParties.Where(x => x.CompanyName == partyName).FirstOrDefaultAsync();
                if (CpartyObj != null)
                {
                    //var deletedBy = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? loggedInUser;
                    CpartyObj.IsDeleted = true;

                    await _context.SaveChangesAsync();
                    return $"ConterParty {partyName} Deleted";
                }
                return $"Counter party not Found";
            }
            catch (Exception)
            {

                throw;
            }
        }// Delete CounterParty By CompanyName

        public async Task<List<CounterParty>> GetAllCounterParties()
        {
            try
            {
                var CpartyList = await _context.CounterParties.Where(x => x.IsDeleted == false).ToListAsync();


                return CpartyList;
            }
            catch (Exception)
            {

                throw;
            }
        }// Getting All CounterParty

        public async Task<string> Update(CounterPartyUpdateDto request)
        {
            try
            {
                var counterParty = await _context.CounterParties.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (counterParty == null)
                {
                    throw new Exception("Counterparty Not Found");
                }

                if (request.ComapnyName != null || request.ComapnyName != "string")

                    counterParty.CompanyName = request.ComapnyName;

                await _context.SaveChangesAsync();
                return "CounterParty Updated";
            }
            catch (Exception)
            {

                throw;
            }
        }// Updating CounterParty

        public async Task<string> ApproveCounterParty(int id, string approval)
        {
            try
            {
                var counterPObj = await _context.CounterParties.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
                if (counterPObj == null)
                {
                    throw new Exception($"Counter Party For This CounterPartyId {id} not Found");
                }
                if (counterPObj.IsApproved == status.Approved.ToString())
                {
                    throw new Exception($"This CounterParty {counterPObj.CompanyName} Already Approved");
                }
                var approvedBy = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? loggedInUser;
                counterPObj.IsApproved = approval;
                counterPObj.ApprovedBy = approvedBy;
                await _context.SaveChangesAsync();

                return $"Counter Party {counterPObj.CompanyName} That's Id Is {id} is {approval}";
            }
            catch (Exception)
            {

                throw;

            }

        }  // Getting Approval of CounterParty By ComplianceMember
    }
}
