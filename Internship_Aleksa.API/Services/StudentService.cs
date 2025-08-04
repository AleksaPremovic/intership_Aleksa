using Internship_Aleksa.Domain.Models;
using Internship_Aleksa.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship_Aleksa.API.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Student>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Student?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task AddAsync(Student student) => _repository.AddAsync(student);
        public Task UpdateAsync(Student student) => _repository.UpdateAsync(student);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
