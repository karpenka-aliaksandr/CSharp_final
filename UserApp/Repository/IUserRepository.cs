using UserApp.DTO;
using UserApp.Model;

namespace UserApp.Repository;

public interface IUserRepository
{
    public void UserAdd(string email, string password);
    public RoleId UserCheck(string name, string password);

    public IEnumerable<MailRoleDTO> GetUsers();
    public void UserDelete(string email);
    public Guid GetUserId(string email);

}