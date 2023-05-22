using DataAccessLayer.DbHelper;
using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using ServicesLayer.IService;

namespace ServicesLayer.Services
{
    public class EventServices:IEventServices
    {
        private readonly IEventMethods _eventMethods;
        private readonly IServicesHelper _servicesHelper;
        
     
        public EventServices(IEventMethods eventMethods, IServicesHelper servicesHelper)
        {
            _eventMethods = eventMethods;
            _servicesHelper = servicesHelper;
        
            
        }
        
        public async Task<string> CreateEvent(EventDto request)
        {
            
            var countries = await _servicesHelper.GetCountries();
            var state = CountryCheck.CheckState(countries, request.CountryName, request.StateName);

            var eventObj = new Event
            {
                Title= request.Title,
                Description= request.Description,
                Agenda= request.Agenda,
                PostalCode= request.PostalCode,
                StartingDate= request.StartingDate,
                EndingDate= request.EndingDate,
                CostPerAttendies= request.CostPerAttendies,
                EstimateCost= request.EstimateCost,
                CreatedBy= "",
                CreatedOn=DateTimeOffset.Now,
                StateId=state.Id, 
                CountryId=state.CountryId
            };

            var res = await _eventMethods.CreateEvent(eventObj, request.HostIds);
            return res;
           
        }

        public async Task<string> UpdateEvent(EventUpdateDto request)
        {
            var res=await _eventMethods.UpdateEvent(request);
            return res;
        }

        public async Task<string> GetApproval(EventApprovalDto request)
        {
            var res= await _eventMethods.GetApproval(request);
            return res;
        }
    }

}
