using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.IService;
using ServicesLayer.RequestDTOs;

namespace APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ContactController : ControllerBase
    {
        private readonly IContactServices _services;
        public ContactController(IContactServices services)
        {
            _services = services;
        }
        
        [HttpDelete]

        public async Task<IActionResult> DeleteContact(int contactId)
        {
            var res = await _services.DeleteContact(contactId);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactRequestDTO request)
        {
            //var createdby = User.FindFirstValue(ClaimTypes.Email);
            //var empId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _services.AddContact(request);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContactByCompanyName(string companyName)
        {
            var res = await _services.GetAllContacts(companyName);
            return Ok(res);
        }

        //[HttpPut]
        //public async Task<IActionResult> ContactUpdate(ContactUpdateDto request)
        //{
        //    var res = await _services.UpdateContact(request);
        //    return Ok(res);
        //}
    }
}
