using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Repositories;

namespace TjommeMetSomme.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            await _unitOfWork.Students.Add(student);

            await _unitOfWork.Commit();

            return student;
        }

        public async Task DeleteStudent(Student student)
        {
            _unitOfWork.Students.Remove(student);

            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Student>> GetAllWithParent()
        {
            return await _unitOfWork.Students
                .GetAllWithParentAsync();
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            return await _unitOfWork.Students
                .GetById(studentId);
        }

        public async Task<Student> GetStudentByIdWithParent(int studentId)
        {
            return await _unitOfWork.Students
                .GetByIdWithParentAsync(studentId);
        }

        public async Task<IEnumerable<Student>> GetStudentsByParentId(int parentId)
        {
            return await _unitOfWork.Students
                .GetAllWithParentByParentIdAsync(parentId);
        }

        public async Task UpdateStudent(Student studentToBeUpdated, Student student)
        {
            studentToBeUpdated.ParentId = student.ParentId;

            await _unitOfWork.Commit();
        }
    }
}