using DataAccessLayer.DbHelper;
using DataAccessLayer.Models;
using ServicesLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.IService
{
    public interface IContactServices
    {
       
        public Task<string> AddContact(ContactRequestDTO request);
        public Task<string> UpdateContact(ContactUpdateDto request);
        public Task<string > DeleteContact(int ContactId);

        public Task<List<Contact>> GetAllContacts(string companyName);
    }
}
