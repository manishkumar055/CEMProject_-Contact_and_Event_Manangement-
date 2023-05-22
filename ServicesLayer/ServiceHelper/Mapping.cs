using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DataAccessLayer.Models;
using DataAccessLayer.RequestDTOs;
using ServicesLayer.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.ServiceHelper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
            CreateMap<ContactAddress, ContactAddressDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();

        }
    }
}
