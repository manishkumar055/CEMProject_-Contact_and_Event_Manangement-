using DataAccessLayer.DbHelper;
using DataAccessLayer.Models;
using ServicesLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDbMethods
{
    public interface IContactMethods
    {
        public Task<string> AddContact(Contact contact);
        public Task<string> DeleteContact(int ContactId);
        public Task<string> UpdateContact(Contact request);
        public Task<List<Contact>> GetAllContacts(string companyName);


        




       
    }
}
