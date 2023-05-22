using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.IService
{
    public interface ICounterPartyServices
    {
        public Task<string> AddCounterParty(string party);
        public Task<string> Update(CounterPartyUpdateDto request);

        public Task<string> DeleteCounterParty(string partyName);
        public Task<List<string>> GetAllCounterParties();
        public Task<string> ApproveCounterParty(int id, string approval);
    }
}
