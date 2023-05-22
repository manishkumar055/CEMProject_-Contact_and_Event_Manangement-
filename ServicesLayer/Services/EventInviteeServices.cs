using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using ServicesLayer.IService;

namespace ServicesLayer.Services
{
    public class EventInviteeServices : IEventInviteeServices
    {
        private readonly IEventInviteeMethods _InviteeMethods;
        public EventInviteeServices(IEventInviteeMethods inviteeMethods)
        {
            _InviteeMethods = inviteeMethods;
        }


        public async Task<string> AddOrDeleteInvitee(EventInviteeRequest request)
        {
            var res = await _InviteeMethods.AddOrDeleteInvitee(request);
            return res;
        }

        public IQueryable<EventInvitee> GetAllInvitees(int eventId)
        {
            var res = _InviteeMethods.GetAllInvitees(eventId);
            return res;
        }

        public async Task<string> GetApproval( InviteeApprovalDto request)
        {
            var res=await _InviteeMethods.GetApproval(request);
            return res;
        }
    }
}
