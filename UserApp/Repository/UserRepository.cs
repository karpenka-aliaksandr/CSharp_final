using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using UserApp.Context;
using UserApp.DTO;
using UserApp.Model;

namespace UserApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private UserContext _userContext;
        public UserRepository(IMapper mapper, UserContext userContext) { 
            _mapper = mapper;
            _userContext = userContext;
        }

        public void UserAdd(string email, string password, RoleId roleId)
        {
            using (_userContext)
            {
                if (roleId == RoleId.Admin)
                {
                    var count = _userContext.Users.Count(x => x.RoleId == RoleId.Admin);
                    if (count > 0)
                    {
                        throw new System.Exception("Admin already exists");
                    }
                }

                var user = new User()
                {
                    Email = email,
                    RoleId = roleId,
                    Salt = new byte[16]
                };

                new Random().NextBytes(user.Salt);
                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();

                SHA512 shaM = new SHA512Managed();
                user.Password = shaM.ComputeHash(data);
                _userContext.Add(user);
                _userContext.SaveChanges();
                
            }
        }

        public RoleType UserCheck(string name, string password)
        {
            using (_userContext)
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == name);
                if (user == null)
                {
                    throw new System.Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var hash = shaM.ComputeHash(data);

                if (hash.SequenceEqual(user.Password))
                {
                    return _mapper.Map<RoleType>(user.RoleId);
                }

                throw new Exception("Wrong password");
            }
        }

    }
}
