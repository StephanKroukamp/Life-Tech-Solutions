using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Repositories;

namespace TjommeMetSomme.Services
{
    public interface ICourseService : IService<Course>
    {
        Task<IEnumerable<Course>> GetAll(bool includeStudents);

        Task<IEnumerable<Course>> GetAllByStudentId(int studentId, bool includeStudents);

        Task<Course> GetById(int courseId, bool includeStudents);
    }

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Course> Create(Course course)
        {
            await _unitOfWork.Courses.Add(course);

            await _unitOfWork.Commit();

            return course;
        }

        public async Task Delete(Course course)
        {
            _unitOfWork.Courses.Remove(course);

            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _unitOfWork.Courses.GetAll();
        }

        public async Task<Course> GetById(int courseId)
        {
            return await _unitOfWork.Courses.GetById(courseId);
        }
        
        public async Task Update(Course courseToBeUpdated, Course course)
        {
            courseToBeUpdated.Name = course.Name;

            await _unitOfWork.Commit();
        }

        //

        public async Task<IEnumerable<Course>> GetAll(bool includeStudents)
        {
            return await _unitOfWork.Courses.GetAll(includeStudents);
        }

        public async Task<Course> GetById(int courseId, bool includeStudents)
        {
            return await _unitOfWork.Courses.GetById(courseId, includeStudents);
        }

        public async Task<IEnumerable<Course>> GetAllByStudentId(int courseId, bool includeStudents)
        {
            return await _unitOfWork.Courses.GetAllByStudentId(courseId, includeStudents);
        }
    }
}