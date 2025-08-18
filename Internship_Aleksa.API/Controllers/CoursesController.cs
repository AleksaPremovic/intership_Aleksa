using Microsoft.AspNetCore.Mvc;
using Internship_Aleksa.Data.FileStorage;
using Internship_Aleksa.Domain.Models;
using Internship_Aleksa.Domain.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Internship_Aleksa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseFileStorage _storage;

        public CoursesController(CourseFileStorage storage)
        {
            _storage = storage;
        }

        // GET: /api/Courses
        [HttpGet]
        public ActionResult<List<CourseDto>> Get()
        {
            var courses = _storage.GetAll();
            var dtos = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                FileName = c.FileName
            }).ToList();
            return Ok(dtos);
        }

        // GET: /api/Courses/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _storage.GetById(id);
            if (course == null) return NotFound();

            var dto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                FileName = course.FileName
            };
            return Ok(dto);
        }

        // POST: /api/Courses
        [HttpPost]
        public IActionResult Post([FromBody] CourseCreateDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                FileName = dto.FileName
            };

            _storage.Add(course);
            return Ok(course); // vraća novi kurs sa generisanim ID-jem
        }

        // PUT: /api/Courses
        [HttpPut]
        public IActionResult Put([FromBody] CourseUpdateDto dto)
        {
            var course = new Course
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                FileName = dto.FileName
            };

            var existing = _storage.GetById(course.Id);
            if (existing == null) return NotFound();

            _storage.Update(course);
            return Ok(course);
        }

        // DELETE: /api/Courses/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _storage.GetById(id);
            if (existing == null) return NotFound();

            _storage.Delete(id);
            return Ok();
        }
    }
}
