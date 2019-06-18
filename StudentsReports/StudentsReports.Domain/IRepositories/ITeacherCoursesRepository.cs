using StudentsReports.Domain.Helpers;
using StudentsReports.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsReports.Domain.IRepositories
{
    public interface ITeacherCoursesRepository
    {
        void Add(TeacherCourses course);
        void Update(TeacherCourses course);
        TeacherCourses GetById(int id);
        bool Exists(TeacherCourses course, int? id = null);
        IEnumerable<TeacherCoursesView> GetAll(Pager pager);
        void AssignToCourse(StudentCourses record);
        void AddSubject(Subjects record);
    }
}
