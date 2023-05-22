using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.IService;
using System.Security.Claims;

namespace APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventInviteeController : ControllerBase
    {
        private readonly IEventInviteeServices _invitee;

        public EventInviteeController(IEventInviteeServices invitee)
        {
            _invitee = invitee;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,EventMember,ComplianceMember,CCEO")]

        public IActionResult GetAllInvittes(int eventId)
        {
            var res = _invitee.GetAllInvitees(eventId);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "EventMember,Admin")]
        public async Task<IActionResult> AddOrDeleteInvitee([FromForm]EventInviteeRequest request)
        {
            
     
            var res = await _invitee.AddOrDeleteInvitee(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "ComplianceMember,CCEO")]

        public async Task<IActionResult> GetApproval([FromForm] InviteeApprovalDto request)
        {
            var createdBy=User.FindFirstValue(ClaimTypes.Email);
            var res = await _invitee.GetApproval(request);
            return Ok(res);
        }



    }
}
