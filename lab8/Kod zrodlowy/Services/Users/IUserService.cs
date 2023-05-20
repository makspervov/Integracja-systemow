using IS_LAB8_JWT.Entities;
using IS_LAB8_JWT.Model;

namespace IS_LAB8_JWT.Services.Users
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        IEnumerable<User> GetUsers();
        User GetByUsername(string username);
        User GetById(int id);
    }
}
