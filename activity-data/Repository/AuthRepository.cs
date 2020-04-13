using System;
using System.Threading.Tasks;
using activity_model;
using Microsoft.EntityFrameworkCore;

namespace activity_data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ActivityDataContext _context;
        public AuthRepository(ActivityDataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x =>x.UserName == userName);
            if (user is null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
               return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for (int i = 0; i < computedHash.Length; i++)
               {
                   if(computedHash[i] != passwordHash[i]) return false;
               }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            await  _context.AddAsync(user);
            await  _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string userName)
        {
            if(await _context.Users.AnyAsync(x => x.UserName == userName))
               return true;

            return false;
        }
    }
}