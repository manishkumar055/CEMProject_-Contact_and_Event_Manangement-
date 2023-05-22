using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDbMethods
{
    public interface IStateMethods
    {
        public Task<string> AddState(string stateName, string CountryName);
        public Task<List<string>> GetStates(string countryName);
    }
}
