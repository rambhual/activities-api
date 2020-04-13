using System.Threading.Tasks;
using activity_model;

namespace activity_data.Repository
{
    public interface IAuthRepository
    {
         Task<User> Register(User user ,string password);
         Task<User> Login(string userName ,string password);
         Task<bool> UserExist(string userName );
    }
}