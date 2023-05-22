using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.IService;

namespace APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   

    public class CounterPartyController : ControllerBase
    {
        private readonly ICounterPartyServices _conterPartyServices;
        public CounterPartyController(ICounterPartyServices counterPartyServices)
        {
            _conterPartyServices = counterPartyServices;
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin,EventMember,Employee")]
        public async Task<IActionResult> AddCounterParty(string counterPartyName)
        {
       
            var res = await _conterPartyServices.AddCounterParty(counterPartyName);
            return Ok(res);
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin,EventMember,ComplianceMember,CCEO")]
        public async Task<IActionResult> GetAllCounterParties()
        {
            var res = await _conterPartyServices.GetAllCounterParties();
            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles = "EventMember,Admin")]
        public async Task<IActionResult> DeleteCounterParty(string counterPartyName)
        {

            var res = await _conterPartyServices.DeleteCounterParty(counterPartyName);
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "EventMember,Admin,Employee")]
        public async Task<IActionResult> Update(CounterPartyUpdateDto request)
        {
            var res = await _conterPartyServices.Update(request);
            return Ok(res);
        }
        [HttpPost] 
        [Authorize(Roles = "ComplianceMember")]
        public async Task<IActionResult> ApproveCounterParty([FromForm] int id, status req)
        {
         
            var res =await _conterPartyServices.ApproveCounterParty(id, req.ToString());
            return Ok(res);

        }
    }
}
