using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Routing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDbMethods
{

    public interface IEventInviteeMethods
    {

        public  Task<string> AddOrDeleteInvitee(EventInviteeRequest request);

       

        public IQueryable<EventInvitee> GetAllInvitees(int eventId);

        public Task<string> GetApproval( InviteeApprovalDto request);


        


    }
}
