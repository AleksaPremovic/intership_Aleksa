using System.Threading.Tasks;
using Internship_Aleksa.API.Services;
using Internship_Aleksa.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Internship_Aleksa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _studentService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            await _studentService.AddAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();

            await _studentService.UpdateAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
