
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbHelper
{
    public class CountryCheck
    {
        public static State CheckState(List<Country> countries, string countryName,string stateName)
        {
            var country= countries.Where(x=>x.Name.Equals(countryName)).FirstOrDefault();
            if (country==null)
            {
                throw new Exception("Invalid Country");
            }
            var state=country.States.FirstOrDefault(x=>x.Name==stateName);
            if (state==null) { throw new Exception($"Inavalid State {stateName}"); };
            return state;
        }// Checking Country then find That state and return 
    }
}
