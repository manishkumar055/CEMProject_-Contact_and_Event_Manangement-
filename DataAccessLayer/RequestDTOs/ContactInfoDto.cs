using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.RequestDTOs
{
    public class ContactInfoDto
    {
        public int Id { get; set; }
        public ContactType ContactType { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public string ContactContent { get; set; }
    }
    public enum IsPrimary
    {
        Yes=1,
        No=2,
    }
    public enum ContactType
    {
        Email = 1,
        MobileNumber=2

    }
    public enum IsDeleted
    {
        No=0,
        Yes=1
    }
}
