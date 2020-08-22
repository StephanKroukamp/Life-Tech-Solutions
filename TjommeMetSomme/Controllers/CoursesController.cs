using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Resources;
using TjommeMetSomme.Services;
using TjommeMetSomme.Validators;
namespace TjommeMetSomme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        private readonly IStudentService _studentService;

        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService, IStudentService studentService, IMapper mapper)
        {
            _mapper = mapper;

            _courseService = courseService;

            _studentService = studentService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CourseResource>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllWithStudents();

            var courseResources = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseResource>>(courses);

            return Ok(courseResources);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CourseResource>> GetCourseByIdWithStudents(int id)
        {
            var course = await _courseService.GetCourseByIdWithStudents(id);

            var courseResource = _mapper.Map<Course, CourseResource>(course);

            return Ok(courseResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<CourseResource>> CreateCourse([FromBody] SaveCourseResource saveCourseResource)
        {
            var validator = new SaveCourseResourceValidator();

            var validationResult = await validator.ValidateAsync(saveCourseResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var courseToCreate = _mapper.Map<SaveCourseResource, Course>(saveCourseResource);

            var newCourse = await _courseService.CreateCourse(courseToCreate);

            var course = await _courseService.GetCourseById(newCourse.CourseId);

            var courseResource = _mapper.Map<Course, CourseResource>(course);

            return Ok(courseResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<StudentResource>> UpdateCourse(int id, [FromBody] SaveCourseResource saveCourseResource)
        {
            var validator = new SaveCourseResourceValidator();

            var validationResult = await validator.ValidateAsync(saveCourseResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var courseToBeUpdated = await _courseService.GetCourseByIdWithStudents(id);

            if (courseToBeUpdated == null)
            {
                return NotFound();
            }

            var course = _mapper.Map<SaveCourseResource, Course>(saveCourseResource);

            await _courseService.UpdateCourse(courseToBeUpdated, course);

            var updatedCourse = await _courseService.GetCourseByIdWithStudents(id);

            var updatedCourseResource = _mapper.Map<Course, CourseResource>(updatedCourse);

            return Ok(updatedCourseResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var course = await _courseService.GetCourseByIdWithStudents(id);

            if (course == null)
            {
                return NotFound();
            }

            await _courseService.DeleteCourse(course);

            return NoContent();
        }

        [HttpPost("{courseId}/Student/{studentId}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            if (studentId == 0 || courseId == 0)
            {
                return BadRequest();
            }

            var courseToBeUpdated = await _courseService.GetCourseByIdWithStudents(courseId);

            if (courseToBeUpdated == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentById(studentId);

            if (student == null)
            {
                return NotFound();
            }

            var course = courseToBeUpdated;

            course.StudentCourses.Add(new StudentCourse
            {
                CourseId = courseId,
                StudentId = studentId
            });

            await _courseService.UpdateCourse(courseToBeUpdated, course);

            var updatedCourse = await _courseService.GetCourseByIdWithStudents(courseId);

            var courseResource = _mapper.Map<Course, CourseResource>(updatedCourse);

            return Ok(courseResource);
        }

        [HttpDelete("{courseId}/Student/{studentId}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> RemoveStudentFromCourse(int courseId, int studentId)
        {
            if (studentId == 0 || courseId == 0)
            {
                return BadRequest();
            }

            var courseToBeUpdated = await _courseService.GetCourseByIdWithStudents(courseId);

            if (courseToBeUpdated == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentById(studentId);

            if (student == null)
            {
                return NotFound();
            }

            var course = courseToBeUpdated;

            var studentCourseToBeRemoved = student.StudentCourses.SingleOrDefault(studentCourse => studentCourse.CourseId == courseId && studentCourse.StudentId == studentId);

            course.StudentCourses.Remove(studentCourseToBeRemoved);

            await _courseService.UpdateCourse(courseToBeUpdated, course);

            var updatedCourse = await _courseService.GetCourseByIdWithStudents(courseId);

            var courseResource = _mapper.Map<Course, CourseResource>(updatedCourse);

            return Ok(courseResource);
        }
    }
}