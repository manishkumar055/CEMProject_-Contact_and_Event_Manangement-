using DataAccessLayer.Models;


namespace Final_Experiment.Helper
{


    public interface ITokenCreation
    {

        public string CreateToken(Employee user);

        //void CreateHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt);
        //public bool VerifyPasswordHash(string password, byte[] passwordsalt, byte[] passwordHash);

    }
}
