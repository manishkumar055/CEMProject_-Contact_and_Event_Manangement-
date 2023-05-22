using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RequestDTOs
{
    public class AddOrDeleteHost
    {
        public int EmployeeId { get; set; }
        public UpdateHost AddOrDeleted { get; set; }

    }
    public enum UpdateHost
    {
        Delete=1,
        Add=2
    }
}
