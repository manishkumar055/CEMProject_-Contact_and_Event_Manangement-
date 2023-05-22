using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbMethods
{
    public class EmployeeMethods: IEmployeeMethods
    {
        private readonly CemContext _context;
        public EmployeeMethods(CemContext context)
        {
            _context = context;
        }

        public async Task<Employee> CheckEmployee(int employeeID)
        {
            try
            {
                var empObj=await _context .Employees.Where(x=>x.Id== employeeID).Include(x=>x.Role).FirstOrDefaultAsync();
                if (empObj==null) {
                    throw new Exception($"EmployeeId {employeeID} not Found");
                }
                return empObj;
            }
            catch (Exception)
            {

                throw;
            }
        }// Cheking EmployeeId For Login 
    }
}
