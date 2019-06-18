using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class ReportsController : ControllerBase
    {
        private IMapper _mapper;
        private IReportsRepository _reportsRepository;
        private IUsersRepository _usersRepository;
        private IHostingEnvironment _hostingEnvironment;

        public ReportsController(IMapper mapper, IReportsRepository reportsRepository, IUsersRepository usersRepository,
            IHostingEnvironment hostingEnvironment)
        {
            this._mapper = mapper;
            this._reportsRepository = reportsRepository;
            this._usersRepository = usersRepository;
            this._hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = UserRoles.Student)]
        [HttpPost]
        public IActionResult Create([FromForm] CreateReport model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!model.File.FileName.ToLower().EndsWith("pdf"))
            {
                return BadRequest(ResponseMessage.IncorrectFileExtension);
            }
            string fileId = Guid.NewGuid().ToString();

            var record = new Domain.Models.Reports();
            record.Date = DateTime.Now;
            record.SubjectId = model.SubjectId;
            record.FileId = fileId;
            record.FileName = model.File.FileName;
            SaveFile(model.File, fileId);

            _reportsRepository.Add(record, model.StudentsIds);

            return Ok();
        }

        private void SaveFile(IFormFile file, string fileId)
        {
            if (file.Length > 0)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Files", fileId);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath = Path.Combine(filePath, file.FileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
            }
        }

        [Authorize(Roles = UserRoles.Teacher)]
        [HttpGet]       
        public IActionResult GetAll([FromQuery] Helpers.Pager query)
        {
            var pager = _mapper.Map<Domain.Helpers.Pager>(query);

            var records = _reportsRepository.GetAll(pager);

            var items = _mapper.Map<List<Models.ReportsView.Reports>>(records);

            var result = new Models.ReportsView
            {
                Items = items,
                TotalItems = pager.TotalItems
            };

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Teacher)]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit([FromBody] UpdateReport model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = _reportsRepository.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            record.Mark = model.Mark;
            record.Comment = model.Comment;

            _reportsRepository.Update(record);

            return Ok();
        }
    }
}