using UserApp.DTO;
using UserApp.Model;

namespace UserApp.Services;

public interface ITokenService
{
    public string GenerateToken(LoginViewModel loginViewModel);
}