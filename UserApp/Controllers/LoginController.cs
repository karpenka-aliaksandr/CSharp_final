using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserApp.DTO;
using UserApp.Model;
using UserApp.Repository;
using UserApp.Services;

namespace UserApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(IUserRepository userRepository, ITokenService tokenService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] MailPasswordDTO userLogin)
        {
            try
            {
                var roleId = userRepository.UserCheck(userLogin.Email, userLogin.Password);

                var user = new MailRoleDTO() { Email = userLogin.Email, Role = (RoleType)roleId };

                var token = tokenService.GenerateToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("adduser")]
        public ActionResult AddUser([FromBody] MailPasswordDTO mailPassword)
        {
            try
            {
                userRepository.UserAdd(mailPassword.Email, mailPassword.Password);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("getusers")]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<DTO.MailRoleDTO> GetUsers()
        {
            return userRepository.GetUsers();
        }


        [HttpDelete]
        [Route("deleteuser")]
        [Authorize(Roles = "Admin")]
        public ActionResult UserDelete([FromBody] MailDTO mailDTO)
        {
            try
            {
                userRepository.UserDelete(mailDTO.Email);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("getcurrentuserid")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult GetUserId()
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var userId = userRepository.GetUserId(userEmail);
                return Ok($"id = {userId}");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
