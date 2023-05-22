using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using ServicesLayer.IService;

namespace ServicesLayer.Services
{
    public class CounterPartyServices:ICounterPartyServices
    {
        private readonly ICounterPartyMethods _conterPartyMethods;
        public CounterPartyServices(ICounterPartyMethods counterPartyMethods)
        {
            _conterPartyMethods= counterPartyMethods;
        }// Custructor

        public async Task<string> AddCounterParty(string party)
        {
            
            var CpartyObj = new CounterParty
            {

                CompanyName= party,
                EmployeeId= 0,
                CreatedBy= ""
                

            };
            var res=await _conterPartyMethods.AddCounterParty(CpartyObj);
            return res;
        }

        public async Task<string> DeleteCounterParty(string partyName)
        {
            var res=await _conterPartyMethods.DeleteCounterParty(partyName);
            return res;
        }

        public async Task<List<string>> GetAllCounterParties()
        {
            var res = await _conterPartyMethods.GetAllCounterParties();
            var lists=new List<string>();
          
            
                foreach (var item in res)
                {
                    lists.Add(item.CompanyName);
                }
                return lists;



        }

        public async Task<string> Update(CounterPartyUpdateDto request)
        {
            var res = await _conterPartyMethods.Update(request);
            return res;
        }

        public async Task<string> ApproveCounterParty(int id, string approval)
        {
            var res=await _conterPartyMethods.ApproveCounterParty(id, approval);
            return res;
        }
    }
}
