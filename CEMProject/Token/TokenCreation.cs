using DataAccessLayer.Models;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Final_Experiment.Helper
{
    public class TokenCreation : ITokenCreation
    {

        //public byte[] passwordSalt;

        //public byte[] passwordHash;

        private readonly IConfiguration _config;
        public TokenCreation(IConfiguration config)
        {
            _config = config;
        }



        public string CreateToken(Employee employee)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,employee.Id.ToString()),new Claim(ClaimTypes.Role,employee.Role.Name),
                new Claim(ClaimTypes.Email,employee.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),

                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return (string.Concat("bearer"," ",jwt));
        }


        //public void CreateHash(string password, out byte[] PasswordHash,out  byte[] PasswordSalt)
        //{



        //    using (var hmac = new HMACSHA512())
        //    {
        //        PasswordSalt = hmac.Key;
        //        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        //    }
        //}

        //public  bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}
    }
}
