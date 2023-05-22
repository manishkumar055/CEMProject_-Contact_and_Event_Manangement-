using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.IService
{
    public interface IStateServices
    {
        public Task<string> AddState(string stateName, string CountryName);
        public Task<List<string>> GetStates(string countryName);
    }
}
