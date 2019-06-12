using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsReports.Domain.IRepositories;
using StudentsReports.Domain.Models;
using StudentsReports.WebApi.Helpers;
using StudentsReports.WebApi.Models;

namespace StudentsReports.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherCoursesController : ControllerBase
    {
        private IMapper _mapper;      
        private ITeacherCoursesRepository _teacherCoursesRepository;
        private IUsersRepository _usersRepository;

        public TeacherCoursesController(IMapper mapper, ITeacherCoursesRepository teacherCoursesRepository, IUsersRepository usersRepository)
        {
            this._mapper = mapper;
            this._teacherCoursesRepository = teacherCoursesRepository;
            this._usersRepository = usersRepository;
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPost]
        public IActionResult Create([FromBody] TeacherCourse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isInRole = _usersRepository.IsInRole(model.UserId, UserRoles.Teacher).GetAwaiter().GetResult();

            if (!isInRole)
            {
                return NotFound();
            }

            var record = _mapper.Map<Domain.Models.TeacherCourses>(model);

            bool exists = _teacherCoursesRepository.Exists(record);

            if (exists)
            {
                return Conflict(ResponseMessage.TeacherCourseAlreadyExists);
            }
            _teacherCoursesRepository.Add(record);

            return Ok();
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit([FromBody] TeacherCourse model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = _teacherCoursesRepository.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            record = _mapper.Map<TeacherCourse, Domain.Models.TeacherCourses>(model, record);

            bool exists = _teacherCoursesRepository.Exists(record, id);

            if (exists)
            {
                return Conflict(ResponseMessage.TeacherCourseAlreadyExists);
            }

            _teacherCoursesRepository.Update(record);

            return Ok();
        }
    }
}