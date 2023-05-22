using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.RequestDTOs
{
    public class InviteeApprovalDto
    {
        public int EventId { get; set; }
        public int[] ContactIds{ get; set; }
        public status Status { get; set; } 
        public IFormFile? File { get; set; }


    }

 

}
