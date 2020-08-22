using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Repositories;

namespace TjommeMetSomme.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Course> CreateCourse(Course course)
        {
            await _unitOfWork.Courses.AddAsync(course);

            await _unitOfWork.CommitAsync();

            return course;
        }

        public async Task DeleteCourse(Course course)
        {
            _unitOfWork.Courses.Remove(course);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithStudents()
        {
            return await _unitOfWork.Courses
                .GetAllWithStudentsAsync();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _unitOfWork.Courses
                .GetByIdAsync(courseId);
        }

        public async Task<Course> GetCourseByIdWithStudents(int courseId)
        {
            return await _unitOfWork.Courses
                .GetByIdWithStudentsAsync(courseId);
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId)
        {
            return await _unitOfWork.Courses
                .GetAllWithStudentsByStudentIdAsync(studentId);
        }

        public async Task UpdateCourse(Course courseToBeUpdated, Course course)
        {
            courseToBeUpdated.Name = course.Name;

            await _unitOfWork.CommitAsync();
        }
    }
}