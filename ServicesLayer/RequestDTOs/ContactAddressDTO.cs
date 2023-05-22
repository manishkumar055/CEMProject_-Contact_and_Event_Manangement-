using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.RequestDTOs
{
    public class ContactAddressDTO
    {
        public string City { get; set; }
       
        public decimal PostalCode { get; set; }
        
        public AddressType AddressType { get; set; }
       
        public IsPrimary IsPrimary { get; set; }
        public string Country { get; set; }

        public string State { get; set; }
       
    }
    public enum AddressType
    {
        Parmanent=1,
        Temporary=2,
        Office=3,
        
    }
 

}
