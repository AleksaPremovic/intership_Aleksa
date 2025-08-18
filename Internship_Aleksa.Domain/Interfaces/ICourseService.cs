using Internship_Aleksa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Aleksa.Domain.Interfaces
{
    public interface ICourseService
    {
        List<Course> GetAll();
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}