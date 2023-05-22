
using DataAccessLayer.IDbMethods;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbMethods
{
    public class StateMethods : IStateMethods
    {
        private readonly CemContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public StateMethods(CemContext context, IHttpContextAccessor httpContextAccessor  )
        {
            _context=context;
            _contextAccessor=httpContextAccessor;
        }// Constructor
        public async Task<string> AddState(string stateName, string countryName)
        {
            try
            {
                var countryObj = await _context.Countries.Where(x=>x.Name.Equals(countryName)).FirstOrDefaultAsync();
                if(countryObj == null)
                {
                    var newCountry = new Country
                    {
                        Name = countryName
                    };
                    await _context.Countries.AddAsync(newCountry);
                    await _context.SaveChangesAsync();
                    await _context.States.AddAsync(new State
                    {
                        Name = stateName,
                        CountryId = newCountry.Id
                    });
                    await _context.SaveChangesAsync();
                    return "State Added";
                }
               await _context.States.AddAsync(new State { Name = stateName,CountryId=countryObj.Id });
                await _context.SaveChangesAsync();
                return "State Added";
            }
            catch (Exception)
            {

                throw;
            }
        }// Adding State By CountryWise

        public async Task<List<string>> GetStates(string countryName)
        {
            try
            {
                var country=await _context.Countries.Where(x=>x.Name==countryName).Include(x=>x.States).FirstOrDefaultAsync();
                if(country == null)
                {
                    throw new Exception($"Country {countryName} Not Found...");
                }
                return country.States.Select(x=>x.Name).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }// Getting All States By CountryName
    }
}
