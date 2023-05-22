using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RequestDTOs
{
    public class CounterPartyUpdateDto
    {
        public int Id { get; set; }
        public string ComapnyName { get; set; }

        public string IsApproved { get; set; }
    }
}
