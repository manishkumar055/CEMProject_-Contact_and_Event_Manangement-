using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDbMethods
{
    public interface ICounterPartyMethods
    {
        public Task<string> AddCounterParty(CounterParty party);
        
        public Task<string> DeleteCounterParty(string partyName);
        public Task<string> Update(CounterPartyUpdateDto request);
        public Task<List<CounterParty>> GetAllCounterParties();
        public Task<string> ApproveCounterParty(int id, string approval);


    }
}
