using Internship_Aleksa.Domain.Interfaces;
using Internship_Aleksa.Domain.Models;

namespace Internship_Aleksa.Data.FileStorage;

public sealed class StudentFileRepository : JsonFileRepositoryBase<Student>, IStudentRepository
{
    public StudentFileRepository(FileStorageOptions options)
        : base(options, "students.json")
    {
    }

    protected override int GetId(Student entity) => entity.Id;
    protected override void SetId(Student entity, int id) => entity.Id = id;
}