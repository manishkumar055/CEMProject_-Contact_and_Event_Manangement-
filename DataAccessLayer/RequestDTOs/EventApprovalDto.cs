using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RequestDTOs
{
    public class EventApprovalDto
    {
        public int EventApprovalId { get; set; } 
        public status Status { get; set; }
        public string?  Comment { get; set; }
        public IFormFile? File { get; set; }


    }
    public enum status
    {
        Approved=1,
        Rejected=2,
        Pending=3,
        NotRequired = 4
    }
}
