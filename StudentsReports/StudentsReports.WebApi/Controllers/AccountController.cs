using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsReports.Domain.IRepositories;
using StudentsReports.WebApi.Helpers;
using StudentsReports.WebApi.Models;

namespace StudentsReports.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUsersRepository _usersRepository;
      
        public AccountController(IUsersRepository usersRepository)
        {       
            _usersRepository = usersRepository;
        }

        [HttpPatch]
        [Route("{id}/changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.UserName != HttpContext.User.Identity.Name)
            {
                return Forbid();
            }

            bool checkPassword = await _usersRepository.CheckPassword(user, model.Password);

            if (!checkPassword)
            {
                return BadRequest(ResponseMessage.IncorrectUserNameOrPassword);
            }

            bool result = await _usersRepository.ChangePassword(user, model.Password, model.NewPassword);

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}