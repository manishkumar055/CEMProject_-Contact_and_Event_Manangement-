using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.IService;

namespace APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateServices _stateServices;
        public StateController(IStateServices stateServices)
        {
            _stateServices= stateServices;
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult >AddState( string state,string Country)
        {
            var res = await _stateServices.AddState(state, Country);
            return Ok(res);
        }
        [HttpGet]
        [Authorize(Roles ="Admin,Employee,EventMember")]
        public async Task<IActionResult> GetAllStates(string countryName)
        {
            var res=await _stateServices.GetStates(countryName);
            return Ok(res);
        }
    }
}
