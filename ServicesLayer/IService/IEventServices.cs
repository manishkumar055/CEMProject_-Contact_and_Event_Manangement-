using DataAccessLayer.RequestDTOs;
using ServicesLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.IService
{
    public interface IEventServices
    {
        public Task<string> CreateEvent(EventDto request);
        public Task<string> UpdateEvent(EventUpdateDto request);
        public Task<string> GetApproval(EventApprovalDto request);
    }
}
