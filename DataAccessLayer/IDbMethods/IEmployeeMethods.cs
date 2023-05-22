using DataAccessLayer.Models;

namespace DataAccessLayer.IDbMethods
{
    public interface IEmployeeMethods
    {
        public Task<Employee> CheckEmployee(int employeeID);
    }
}
