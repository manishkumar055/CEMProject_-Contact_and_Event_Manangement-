using DataAccessLayer.ChangeTrack;
using DataAccessLayer.DbHelper;
using DataAccessLayer.Email;
using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace DataAccessLayer.DbMethods
{
    public class EventInviteeMethods : IEventInviteeMethods
    {
        private readonly CemContext _context;
        private readonly IEmailInjection _email;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IChangeTracking _track;
        public EventInviteeMethods(IChangeTracking changeTracking,CemContext context, IEmailInjection email, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _email = email;
            _config = configuration;
            _httpContext = httpContextAccessor;
            _track = changeTracking;
        }//Cunstructor

        public IQueryable<EventInvitee> GetAllInvitees(int eventId)
        {
            try
            {
                var eventObj = _context.EventInvitees.Where(x => x.EventId == eventId).AsQueryable();

                return eventObj;
            }
            catch (Exception)
            {

                throw;
            }
        }// Returning All Invitees By EventId

        public async Task<string> AddOrDeleteInvitee(EventInviteeRequest request)
        {
            try
            {
                var createdBy = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "system";
                var eventId = request.EventId;

                var contactId = request.ContactId;
                var eventObj = await _context.Events.Where(x => x.Id == eventId).Include(x => x.EventInvitees).FirstOrDefaultAsync();
                if (eventObj == null)
                {
                    throw new Exception("Event Not Found");
                }
                if (request.Want_Delete.ToString() == Del.No.ToString())
                {
                    var isExist = eventObj.EventInvitees.FirstOrDefault(x => x.ContactId == contactId);
                    if (isExist != null)
                    {
                        throw new Exception($"This Invitee is Already Added in this Event");
                    }
                    var contactObj = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contactId && x.IsDeleted == false) ;
                    if (contactObj == null)
                    {
                        throw new Exception("Contact Not Found Please Add Contact First...!");
                    }
                    eventObj.EventInvitees.Add(new EventInvitee
                    {
                        ContactId = contactId,
                        EventId = eventId,
                        Status = status.Pending.ToString(),
                        CreatedBy = createdBy,
                        CreatedOn = DateTimeOffset.Now
                    });
                    _track.EventChangesPush();
                    await _context.SaveChangesAsync();
                    return "Invitee Added..!";
                }
                else
                {
                    var inviteeToDeleteObj = eventObj.EventInvitees.FirstOrDefault(x => x.ContactId == contactId);
                    if (inviteeToDeleteObj == null)
                    {
                        throw new Exception("Invitee ContactId Not Found for this Event");
                    }
                    _context.EventInvitees.Remove(inviteeToDeleteObj);
                    _track.EventChangesPush();
                    await _context.SaveChangesAsync();
                    return $"Invitee Deleted id= {contactId} For EventId {eventId} ";
                }


            }
            catch (Exception)
            {

                throw;
            }
        }// Adding Or Deleting Invitee 

        public async Task<string> GetApproval(InviteeApprovalDto request)
        {
            try
            {
                var createdBy= _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.Email)??"system";
                var eventobj = await _context.Events.Where(x => x.Id == request.EventId).Include(x => x.EventInvitees).FirstOrDefaultAsync();
                if (eventobj == null)
                {
                    throw new Exception($"Event Not Found For EventId {request.EventId}");
                }
                var inviteeLists = eventobj.EventInvitees;
                foreach (var InviteeId in request.ContactIds)
                {
                    var inviteeObj = inviteeLists.FirstOrDefault(x => x.Id == InviteeId);
                    if (inviteeObj == null)
                    {
                        throw new Exception($"InviteeId {InviteeId} Not Found For this Event ");
                    }


                    inviteeObj.Status = request.Status.ToString();
               
                    inviteeObj.CreatedOn = DateTimeOffset.Now;

                }
                var email = new EmailDto
                {
                    To = "mk5614355@gmail.com"
                };
                await _email.SendEmailBySendGrid(email);
                if (request.File != null)
                {
                    var filepath = await FIleHandling.FileUpload(_config, request.File);
                    await _context.EventAttachments.AddAsync(new EventAttachment
                    {
                        AttachmentPath = filepath,
                        EventId = request.EventId,
                        EventInviteeId = 0,
                        AttachmentName = request.File.FileName,
                        CreatedBy= createdBy
                    });
                }
                await _context.SaveChangesAsync();
                return "Status Changed ";

            }
            catch (Exception)
            {

                throw;

            }
        }// Getting Approval for Invitees By ComplianceMember

    }
}
