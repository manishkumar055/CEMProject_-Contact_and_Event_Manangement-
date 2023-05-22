using DataAccessLayer.Email;
using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.IService;

namespace APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class EventController : ControllerBase
    {
        private readonly IEventServices _eventServices;
        private readonly IConfiguration _config;
        private IEmailInjection _email;
        public EventController(IEventServices eventServices, IConfiguration configuration, IEmailInjection email)
        {
            _eventServices = eventServices;
            _config = configuration;
            _email = email;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,EventManager")]

        public async Task<IActionResult> CreateEvent(EventDto request)
        {
            

            var res = await _eventServices.CreateEvent(request);
            return Ok(res);
        }
        [HttpPut]
        [Authorize(Roles = "Admin,EventManager")]

        public async Task<IActionResult> UpdateEvent(EventUpdateDto request)
        {
            
            var res = await _eventServices.UpdateEvent(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "CCEO,ComplianceMember")]
        public async Task<IActionResult> GetApproval([FromForm] EventApprovalDto request)
        {
            var res = await _eventServices.GetApproval(request);
            return Ok(res);
        }
        
       
       
    }
}
