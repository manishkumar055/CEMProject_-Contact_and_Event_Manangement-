using DataAccessLayer.IDbMethods;
using ServicesLayer.IService;

namespace ServicesLayer.Services
{
    public class StateServices:IStateServices
    {
        private readonly IStateMethods _stateMethods;
        public StateServices(IStateMethods stateMethods)
        {
            _stateMethods = stateMethods;
        }

        public async Task<string> AddState(string stateName, string CountryName)
        {
            var res=await _stateMethods.AddState(stateName, CountryName);
            return res;
        }

        public async Task<List<string>> GetStates(string countryName)
        {
           var res=await _stateMethods.GetStates( countryName);
            return res;
        }
    }
}
