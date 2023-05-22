using DataAccessLayer.IDbMethods;
using Final_Experiment.Helper;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeMethods _emp;
        private readonly ITokenCreation _token;
        public EmployeeController(IEmployeeMethods employeeMethods, ITokenCreation tokenCreation)
        {
            _emp= employeeMethods;
            _token= tokenCreation;
        }

        [HttpPost]

        public async Task<IActionResult> Login(int id)
        {
            var emp= await _emp.CheckEmployee(id);
            var token = _token.CreateToken(emp);
            return Ok(token);

        }

    }
}
