using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.RequestDTOs
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public prefixes Pre { get; set; }
     
        public string FirstName { get; set; }
  
        public string LastName { get; set; }
   
        public string Title { get; set; }
       
   
        public string? Category { get; set; }

    }
    public enum prefixes
    {
        
        Mr=1,
        Mrs=2,
    }
    

}
