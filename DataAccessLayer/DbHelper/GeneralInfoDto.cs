using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.RequestDTOs
{
    public class GeneralInfoDto
    {

        public string Country { get; set; }=string.Empty;

        public string State { get; set; } = string.Empty;
    }
}
