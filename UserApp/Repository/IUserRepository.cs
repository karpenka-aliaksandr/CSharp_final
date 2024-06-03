using UserApp.DTO;
using UserApp.Model;

namespace UserApp.Repository;

public interface IUserRepository
{
    public void UserAdd(string email, string password, RoleId roleId);
    public RoleType UserCheck(string name, string password);
}