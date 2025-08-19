using Internship_Aleksa.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Internship_Aleksa.Data.FileStorage
{
    public class CourseFileStorage
    {
        private readonly string filePath;

        public CourseFileStorage()
        {
            filePath = Path.Combine(AppContext.BaseDirectory, "courses.json");

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public List<Course> GetAll()
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Course>>(json) ?? new List<Course>();
        }

       
        public Course GetById(int id)
        {
            var courses = GetAll();
            return courses.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            var courses = GetAll();

            int newId = courses.Any() ? courses.Max(c => c.Id) + 1 : 1;
            course.Id = newId;

            courses.Add(course);
            File.WriteAllText(filePath, JsonSerializer.Serialize(courses));
        }

        public void Update(Course course)
        {
            var courses = GetAll();
            var index = courses.FindIndex(c => c.Id == course.Id);
            if (index >= 0)
            {
                courses[index] = course;
                File.WriteAllText(filePath, JsonSerializer.Serialize(courses));
            }
        }

        public void Delete(int id)
        {
            var courses = GetAll();
            courses.RemoveAll(c => c.Id == id);
            File.WriteAllText(filePath, JsonSerializer.Serialize(courses));
        }
    }
}
