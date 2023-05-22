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
        public prefixes Pre { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
   
        public string Title { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string? Category { get; set; } = null!;

    }
    public enum prefixes
    {
        
        Mr=1,
        Mrs=2,
    }
    

}
