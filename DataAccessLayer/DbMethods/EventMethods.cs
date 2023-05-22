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
    public class EventMethods : IEventMethods
    {
        private int maxEventCost = 10000;
        private readonly CemContext _context;
        private readonly IEmailInjection _emailService;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpAcc;
        private readonly IChangeTracking _track;

        public EventMethods(IChangeTracking changeTracking,CemContext cemContext, IEmailInjection emailInjection, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = cemContext;
            _emailService = emailInjection;
            _config = configuration;
            _httpAcc = httpContextAccessor;
            _track = changeTracking;
        }//Cunstructor

        public async Task<string> CreateEvent(Event request, int[] hostsIds)
        {
            try
            {
                var createdBy = _httpAcc.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "system";
                var hostLists = new List<Host>();

                var employeeLists = await _context.Employees.ToListAsync();

                foreach (var id in hostsIds)
                {
                    var employee = employeeLists.FirstOrDefault(x => x.Id == id);
                    if (employee == null)
                    {
                        throw new Exception($"Host ContactId {id} Not Found");

                    }
                    hostLists.Add(new Host
                    {
                        EmployeeId = employee.Id,
                        CreatedBy= createdBy,

                    });
                }


                var TotalBudget = request.EstimateCost;
                if (TotalBudget >= maxEventCost)
                {
                    request.Status = status.Pending.ToString();
                    request.EventApprovals.Add(new EventApproval
                    {
                        CreatedBy = createdBy
                    });
                    var empObj = await _context.Employees.FirstOrDefaultAsync(x => x.Role.Name == "ComplianceMember");
                    if (empObj != null)
                    {
                        await _emailService.SendEmailBySendGrid(new EmailDto
                        {
                            To=empObj.Email,
                            Body="About to Approve Event",
                            Message="Please Approve This Event"
                        });

                    }
                }
                request.CreatedBy= createdBy;
                request.Hosts = hostLists;
                await _context.Events.AddAsync(request);
                await _context.SaveChangesAsync();
                return "Event Created and Send Email to ComplianceMember For Approval";

            }
            catch (Exception)
            {

                throw;
            }
        }// Creating Event By EventManager Or Admin

        public async Task<string> GetApproval(EventApprovalDto request)
        {
            try
            {
                var eventApprovalObj = await _context.EventApprovals.Where(x => x.Id == request.EventApprovalId && x.IsDeleted == false)
                                                                    .Include(x => x.Event)
                                                                    .FirstOrDefaultAsync();
                var emailData = _httpAcc.HttpContext?.User.FindFirstValue(ClaimTypes.Email)??"system";
                if (eventApprovalObj == null)
                {
                    throw new Exception($"EventApproval For Appraval ContactId {request.EventApprovalId} not Found");
                }
                var EmpObj = await _context.Employees.FirstOrDefaultAsync(x => x.Role.Name == "CCEO");
                var email = new EmailDto
                {
                    To = EmpObj.Email,
                    Message = "Thankyou",
                    Body = $" Please approve the Event {eventApprovalObj.Event.Title} ComplienceTeam Already Apprved"

                };
                if (eventApprovalObj.ComplianceTeamStatus == status.Rejected.ToString())
                {
                    throw new Exception("Sorry This EventApproval is Cancelled by ComplianceTeam");
                }
                else if (eventApprovalObj.ComplianceTeamStatus == status.Pending.ToString())
                {
                    if (request.Status.ToString().Equals(status.Rejected.ToString()))
                    {
                        eventApprovalObj.ChiefStatus = request.Status.ToString();
                    }
                    eventApprovalObj.ComplianceEmail = emailData;
                    eventApprovalObj.ComplianceTeamStatus = request.Status.ToString();
                    eventApprovalObj.CommentByCompliance = request.Comment;
                    if (request.Status.Equals(status.Approved))
                        await _emailService.SendEmailBySendGrid(email);
                    if (request.File != null)
                    {
                        var filepath = await FIleHandling.FileUpload(_config, request.File);
                        await _context.EventAttachments.AddAsync(new EventAttachment
                        {
                            AttachmentPath = filepath,
                            EventId = eventApprovalObj.EventId,
                            AttachmentName = request.File.Name,

                        });
                    }
                }
                else
                {
                    if (eventApprovalObj.ChiefStatus == "Approved")
                        throw new Exception("This EventApproval is Aproved...❤❤❤");
                    var empObj = await _context.Employees.Where(x => x.Email.Equals(emailData)).Include(x => x.Role).FirstOrDefaultAsync();
                    if (empObj.Role.Name != "CCEO")
                    {
                        throw new Exception("Only CCEO Can Approve this Aproval");
                    }
                    else if (request.Status.Equals(status.Rejected))
                    {
                        eventApprovalObj.ComplianceTeamStatus = status.Pending.ToString();
                    }
                    //email.Body += "Alse Approved By Compallinace Team";
                    //await _emailService.SendEmailBySendGrid(email);
                    eventApprovalObj.ChiefStatus = request.Status.ToString();
                    eventApprovalObj.Event.Status = status.Approved.ToString();
                    eventApprovalObj.CommentByChief = request.Comment;
                    eventApprovalObj.ChiefEmail = emailData;
                    if (request.File != null)
                    {
                        var filepath = await FIleHandling.FileUpload(_config, request.File);
                        await _context.EventAttachments.AddAsync(new EventAttachment
                        {
                            AttachmentPath = filepath,
                            EventId = eventApprovalObj.EventId,
                            AttachmentName = request.File.Name,

                        });
                    }
                }
                await _context.SaveChangesAsync();
                return "Approval Status Changed";
            }
            catch (Exception)
            {

                throw;
            }
        }// Getting Approval of Event by CompianceMember and CCEO


        public async Task<string> UpdateEvent(EventUpdateDto request)
        {
            try
            {
                var eventObj = await _context.Events.Where(x => x.Id == request.EventId)
                                                    .Include(x => x.EventApprovals)
                                                    .Include(x => x.Hosts)
                                                    .FirstOrDefaultAsync();
                var updatedBy = _httpAcc.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "system";

                if (eventObj == null)
                {
                    throw new Exception($"Event not Found For Event ContactId {request.EventId}");
                }


                var employees = await _context.Employees.ToListAsync();

                var hosts = eventObj.Hosts;
                foreach (var employeeId in request.Employees)
                {
                    var hostObj = hosts.FirstOrDefault(x => x.EmployeeId == employeeId.EmployeeId);


                    if (employeeId.AddOrDeleted.Equals(UpdateHost.Add) && hostObj == null)
                    {
                        var emObj = employees.FirstOrDefault(x => x.Id == employeeId.EmployeeId);
                        if (emObj == null) { throw new Exception($"Employee ContactId Not Found {employeeId}"); }
                        hosts.Add(new Host
                        {
                            EmployeeId = emObj.Id,
                            //EventId = request.EventId,
                            HostType="HostHelper",
                        });
                    }
                    if (hostObj == null && employeeId.AddOrDeleted.Equals(UpdateHost.Delete))
                    {
                        throw new Exception("This Employee Is Not a Host Of this Event so Cant Remove");
                    }
                    else
                    {
                        if (hostObj != null)
                        {
                            _context.Hosts.Remove(hostObj);
                        }
                        //hosts.Remove(hostObj);
                    }

                }
                var totalBudget = request.EstimateCost;
                var eventAprovals = eventObj.EventApprovals;
                if (totalBudget < maxEventCost)
                {
                    eventObj.Status = status.NotRequired.ToString();
                    foreach (var approval in eventAprovals)
                    {
                        approval.IsDeleted = true;
                    }
                    //eventObj.EventApprovals = new List<EventApproval>();
                }
                else
                {

                    eventObj.Status = status.Pending.ToString();

                }

                var countries = await _context.Countries.Where(x => x.Name == request.CountryName).Include(x => x.States).ToListAsync();
                var state = CountryCheck.CheckState(countries, request.CountryName, request.StateName);

                eventObj.StateId = state.Id;
                eventObj.CountryId = state.CountryId;


                eventObj.UpdatedOn = request.UpdatedOn;
                eventObj.UpdatedBy = "system";

                eventObj.Agenda = request.Agenda;
                eventObj.CostPerAttendies = request.CostPerAttendies;
                eventObj.Description = request.Description;
                eventObj.PostalCode = request.PostalCode;
                eventObj.UpdatedBy = updatedBy;
                eventObj.UpdatedOn = DateTimeOffset.Now;

                _track.EventChangesPush();

                await _context.SaveChangesAsync();
                return $" Event Details For EventId {request.EventId} Updated";

            }
            catch (Exception)
            {

                throw;
            }

        }// Updating Event Details 
    }
}
