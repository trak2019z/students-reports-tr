using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.IRepositories;
using StudentsReports.Domain.Models;
using StudentsReports.WebApi.Helpers;
using StudentsReports.WebApi.Models;
using UsersView = StudentsReports.WebApi.Models.UsersView;

namespace StudentsReports.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]    
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersRepository _usersRepository;
        private IRolesRepository _rolesRepository;
        private IMapper _mapper;

        public UsersController(IMapper mapper, IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> Create([FromBody] CreateUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = await _usersRepository.GetByName(model.UserName);
            if (user != null)
            {
                return Conflict(ResponseMessage.UserAlreadyExists);
            }

            user = _mapper.Map<Users>(model);
            user.DateCreated = user.DateModified = DateTime.Now;
            await _usersRepository.Add(user, model.Password, model.RoleId);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromBody] UpdateUser model, string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = await _usersRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (!HttpContext.User.IsInRole(UserRoles.Administrator) && (HttpContext.User.Identity.Name != user.UserName))
            {
                return Forbid();
            }

            IdentityRole role = await _rolesRepository.GetById(model.RoleId);

            if (role == null)
            {
                return BadRequest(ResponseMessage.RoleNotExists);
            }

            user = _mapper.Map<UpdateUser, Users>(model, user);
            user.DateModified = DateTime.Now;

            await _usersRepository.Update(user, model.RoleId);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> GetAll([FromQuery] Helpers.Pager query)
        {           
            var pager = _mapper.Map<Domain.Helpers.Pager>(query);

            var users = _usersRepository.GetAll(pager);

            List<Models.UsersView.Users> items = _mapper.Map<List<Models.UsersView.Users>>(users);

            var result = new UsersView
            {
                Items = items,
                TotalItems = pager.TotalItems
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IActionResult> Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = await _usersRepository.GetDetails(id);

            if (user == null)
            {
                return NotFound();
            }

            UsersView result = _mapper.Map<Models.UsersView>(user);
           
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = await _usersRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _usersRepository.Delete(user);

            return Ok();
        }
    }
}