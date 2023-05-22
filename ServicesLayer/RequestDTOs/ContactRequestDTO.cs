using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.RequestDTOs
{
    public  class ContactRequestDTO
    {
        public ContactDto Person { get; set; }
        public ContactInfoDTO[] Information { get; set; }
        public ContactAddressDTO [] Addresses { get; set; }
        
      
    }
}
