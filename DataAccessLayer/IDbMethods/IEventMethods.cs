using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using ServicesLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDbMethods
{
    public interface IEventMethods
    {
        public Task<string> CreateEvent(Event request,int [] hostsIds );
        public Task<string> UpdateEvent(EventUpdateDto request);

        public Task<string> GetApproval(EventApprovalDto request);
        
    }
}
