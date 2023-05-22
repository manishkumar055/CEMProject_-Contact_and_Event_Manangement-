using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Email
{
    public class EmailDto
    {
        public string To { get; set; }
        public string Body { get; set; } = "";
        //public int EmpIdToSendMail { get; set; }
        public string Message { get; set; } = "";

       // public int EventAprovalId { get; set; }


    }
}
