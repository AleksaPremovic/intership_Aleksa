using Internship_Aleksa.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship_Aleksa.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
