using System.Collections.Generic;
using System.Threading.Tasks;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Repositories;

namespace TjommeMetSomme.Services
{
    public interface IStudentService : IService<Student>
    {
        Task<IEnumerable<Student>> GetAll(bool includeParent);

        Task<IEnumerable<Student>> GetAllByParentId(int parentId, bool includeParent);

        Task<Student> GetById(int studentId, bool includeParent);
    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Student> Create(Student student)
        {
            await _unitOfWork.Students.Add(student);

            await _unitOfWork.Commit();

            return student;
        }

        public async Task Delete(Student student)
        {
            _unitOfWork.Students.Remove(student);

            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _unitOfWork.Students.GetAll();
        }

        public async Task<Student> GetById(int studentId)
        {
            return await _unitOfWork.Students.GetById(studentId);
        }

        public async Task Update(Student studentToBeUpdated, Student student)
        {
            studentToBeUpdated.ParentId = student.ParentId;

            await _unitOfWork.Commit();
        }

        // 

        public async Task<IEnumerable<Student>> GetAll(bool includeParent)
        {
            return await _unitOfWork.Students.GetAll(includeParent);
        }

        public async Task<IEnumerable<Student>> GetAllByParentId(int parentId, bool includeParent)
        {
            return await _unitOfWork.Students.GetAllByParentId(parentId, includeParent);
        }

        public async Task<Student> GetById(int studentId, bool includeParent)
        {
            return await _unitOfWork.Students.GetById(studentId, includeParent);
        }
    }
}