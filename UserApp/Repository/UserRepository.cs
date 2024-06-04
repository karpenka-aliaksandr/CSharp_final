using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserContext _userContext;
        public UserRepository(IMapper mapper, UserContext userContext) { 
            _mapper = mapper;
            _userContext = userContext;
        }

        public void UserAdd(string email, string password)
        {
            using (_userContext)
            {
                if (_userContext.Users.Any(x => x.Email == email))
                {
                    throw new Exception("This email already exist");
                }
                var roleId = RoleId.Admin;
                if (_userContext.Users.Any(x => x.RoleId == roleId))
                {
                    roleId = RoleId.User;
                }
                var user = new Model.User()
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

        public RoleId UserCheck(string name, string password)
        {
            using (_userContext)
            {
                var user = _userContext.Users.FirstOrDefault(x => x.Email == name);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var hash = shaM.ComputeHash(data);

                if (hash.SequenceEqual(user.Password))
                {
                    return user.RoleId;
                }

                throw new Exception("Wrong password");
            }
        }

        public IEnumerable<MailRoleDTO> GetUsers()
        {
            using (_userContext)
            {
                return _userContext.Users.Select(_mapper.Map<MailRoleDTO>).ToList();
            }
        }

        public void UserDelete(string email)
        {
            var user = _userContext.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.RoleId == RoleId.Admin)
            {
                var count = _userContext.Users.Count(x => x.RoleId == RoleId.Admin);
                if (count == 1)
                {
                    throw new Exception("You can't delete admin");
                }
            }
            _userContext.Remove(user);
            _userContext.SaveChanges();
        }

        public Guid GetUserId(string email)
        {
            var user = _userContext.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user.Id;
            
        }
    }
}
