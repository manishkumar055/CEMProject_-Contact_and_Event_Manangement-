using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbHelper
{
    public interface IServicesHelper
    {
        public Task<List<Country>> GetCountries();
        public Task<CounterParty> ChekCounterParty(string companyName);
        public Task<Contact> ChekContact(int contactId);
    }
}
