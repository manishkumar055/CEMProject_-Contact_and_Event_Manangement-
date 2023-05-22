using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.IService
{
    public interface IEventInviteeServices
    {
        public Task<string> AddOrDeleteInvitee(EventInviteeRequest request);

        public IQueryable<EventInvitee> GetAllInvitees(int eventId);
        public  Task<string> GetApproval( InviteeApprovalDto request);
    }
}
