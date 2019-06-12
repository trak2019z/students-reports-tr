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
    public class CoursesController : ControllerBase
    {
        private ICoursesRepository _coursesRepository;
        private IMapper _mapper;

        public CoursesController(IMapper mapper, ICoursesRepository coursesRepository)
        {
            this._mapper = mapper;
            this._coursesRepository = coursesRepository;
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPost]
        public IActionResult Create([FromBody] Course model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = _coursesRepository.GetByName(model.Name);
            if (record != null)
            {
                return Conflict(ResponseMessage.CourseAlreadyExists);
            }

            record = _mapper.Map<Courses>(model);

            _coursesRepository.Add(record);

            return Ok();
        }

        [Authorize(Roles= UserRoles.Administrator)]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit([FromBody] Course model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = _coursesRepository.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            record = _mapper.Map<Course, Domain.Models.Courses>(model, record);

            _coursesRepository.Update(record);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var records = _coursesRepository.GetAll();

            var result = _mapper.Map<List<Models.CoursesView>>(records);

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var record = _coursesRepository.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            _coursesRepository.Delete(record);

            return Ok();
        }
    }
}