using AutoMapper;
using DataAccessLayer.ChangeTrack;
using DataAccessLayer.DbHelper;
using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using Microsoft.IdentityModel.Tokens;
using ServicesLayer.IService;
using ServicesLayer.RequestDTOs;
using System.IO.Pipelines;

namespace ServicesLayer.Services
{
    public class ContactServices : IContactServices 
    {
        private readonly IContactMethods _contactMethods;
        private readonly IServicesHelper _help;
        private readonly IChangeTracking _track;
        private readonly IMapper _mapper;

        public ContactServices(IContactMethods contactMethods, IServicesHelper helpService, IChangeTracking changeTracking, IMapper mapper)
        {
            _contactMethods = contactMethods;
            _help = helpService;
            _track = changeTracking;
            _mapper = mapper;

        }

        public async Task<string> AddContact(ContactRequestDTO request)
        {
            try
            {


                var person = request.Person;

                var counterparty = await _help.ChekCounterParty(person.CompanyName);
                if(counterparty != null)
                {

                }
                if (counterparty.IsApproved != status.Approved.ToString())
                {

                    throw new Exception("This CounterParty is Not Approved");
                }



                var newContact = new Contact()
                {
                    Prefix = person.Pre.ToString(),
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    CounterPartyId = counterparty.Id,
                    Title = person.Title,
                    EmployeeId = 0,
                    CreatedBy = "system",
                    Category = person.Category
                };
                //var newContact = _mapper.Map<Contact>(person);
                //newContact.Prefix = person.Pre.ToString();

                var contactInfoDTOs = request.Information;
                var contactAddressDTOs = request.Addresses;

                List<ContactAddress> contactAddresses = new();

                var flag = false;

                var countriesWithStates = await _help.GetCountries();
                foreach (var contactAddress in contactAddressDTOs)
                {
                    var state = CountryCheck.CheckState(countriesWithStates, contactAddress.Country, contactAddress.State);

                    var isPrimary2 = false;
                    if (contactAddress.IsPrimary.ToString().Equals("Yes") && !flag)
                    {
                        isPrimary2 = true;
                        flag = true;
                    }
                    contactAddresses.Add(new ContactAddress
                    {
                        ContactId = 0,
                        CountryId = state.CountryId,
                        StateId = state.Id,
                        City = contactAddress.City,
                        PostalCode = contactAddress.PostalCode,
                        AddressType = contactAddress.AddressType.ToString(),
                        IsPrimary = isPrimary2,
                    });


                }




                List<ContactInfo> contactinfoList = new();
                flag = false;

                foreach (var contactInfo in contactInfoDTOs)
                {
                    var isPrimary = false;
                    if (contactInfo.IsPrimary.ToString().Equals("Yes") && !flag)
                    {
                        isPrimary = true;
                        flag = true;
                    }
                    contactinfoList.Add(new ContactInfo
                    {
                        ContactId = 0,
                        ContactType = contactInfo.ContactType.ToString(),
                        ContactContent = contactInfo.ContactContent,
                        IsPrimary = isPrimary,

                    });
                }

                newContact.ContactAddresses = contactAddresses;
                newContact.ContactInfos= contactinfoList;

                var res = await _contactMethods.AddContact(newContact);
                return res;

            }
            catch (Exception)
            {

                throw;
            }

        }// Generating Contact and Info's & Add's then Adding in Contact then Call DB Method to add Contact



        public async Task<string> DeleteContact(int ContactId)
        {
            var res = await _contactMethods.DeleteContact(ContactId);
            return res;
        }  // Deleteing Contact By ContactId


        public async Task<List<Contact>> GetAllContacts(string companyName)
        {
            var res = await _contactMethods.GetAllContacts(companyName);
            return res;
        }// Getting All Contacts By CompanyName


        public async Task<string> UpdateContact(ContactUpdateDto request)
        {

            var contactObj = await _help.ChekContact(request.Person.ContactId);
            var clone = contactObj.Clone() as Contact;

            foreach (var Info in request.Information)
            {





                bool sign = false;
                if (Info.IsDeleted.ToString().Equals(IsPrimary.Yes.ToString()))
                {
                    sign = true;
                }
                if (Info.Id == 0)
                {
                    var content = contactObj.ContactInfos.Where(x => x.ContactContent.Equals(Info.ContactContent)).FirstOrDefault();
                    if (content == null)
                    {
                        contactObj.ContactInfos.Add(new ContactInfo
                        {

                            ContactType = Info.ContactType.ToString(),
                            IsDeleted = sign,
                            ContactContent = Info.ContactContent,
                            IsPrimary = false


                        });
                    }

                    else
                        content.IsDeleted = false;

                }

                else
                {
                    var contactInfoObj = contactObj.ContactInfos.FirstOrDefault(x => x.Id == Info.Id);
                    if (contactInfoObj == null)
                    {
                        throw new Exception($"ContactId Not Found For ContactInfo ContactId {Info.Id}");
                    }
                    else if (contactInfoObj.ContactContent.Equals(Info.ContactContent))
                    {
                        contactInfoObj.IsDeleted = sign;    // Doubt
                    }
                    else
                    {
                        contactInfoObj.ContactType = Info.ContactType.ToString();
                        var isDel = false;
                        if (Info.IsDeleted.ToString() == "Yes")
                        {
                            isDel = true;
                        }

                        contactInfoObj.IsDeleted = isDel;

                        contactInfoObj.ContactType = Info.ContactType.ToString();
                        if (!Info.ContactContent.IsNullOrEmpty() && !Info.ContactContent.Equals("string"))
                        {
                            contactInfoObj.ContactContent = Info.ContactContent;
                        }
                    }

                }
            }

            var countryWithStates = await _help.GetCountries();





          
            foreach (var address in request.Addresses)
            {
                var state = CountryCheck.CheckState(countryWithStates, address.Country, address.State);


                var flag = false;
                if (address.IsDeleted.ToString().Equals("Yes"))
                    flag = true;
                if (address.Id == 0)
                {
                    var addressObj = contactObj.ContactAddresses.Where(x => x.City == address.City && x.PostalCode == address.PostalCode);
                    if (addressObj == null)
                        contactObj.ContactAddresses.Add(new ContactAddress
                        {
                            
                            StateId = state.Id,
                            CountryId = state.CountryId,
                            City = address.City,
                            PostalCode = address.PostalCode,
                            AddressType = address.AddressType.ToString(),
                            IsPrimary = false,
                            IsDeleted = flag

                        });
                    
                }
                else
                {
                    var addressObj = contactObj.ContactAddresses.FirstOrDefault(x => x.Id == address.Id);
                    if (addressObj == null)
                    {
                        throw new Exception("Address ContactId Not Found ");
                    }

                    addressObj.StateId = state.Id;
                    addressObj.CountryId = state.CountryId;
                    addressObj.IsDeleted = false;
                    addressObj.City = address.City;
                    addressObj.AddressType = address.AddressType.ToString();
                    addressObj.IsPrimary = false;
                    addressObj.IsDeleted = flag;
                }
            }
            contactObj.Prefix = request.Person.Pre.ToString();
            contactObj.FirstName=request.Person.FirstName;
            contactObj.LastName = request.Person.LastName;
            contactObj.Title= request.Person.Title;
            contactObj.UpdatedOn = DateTimeOffset.Now;



             _track.ContactChangesPush();

            


            var res = await _contactMethods.UpdateContact(contactObj);
            return res;
        } // Contact Updating
    }
}
