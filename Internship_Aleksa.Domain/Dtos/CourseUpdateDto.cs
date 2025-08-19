using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Aleksa.Domain.Dtos
{
    public class CourseUpdateDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
    }
}