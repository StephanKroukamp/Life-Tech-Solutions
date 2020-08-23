using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Entities.Identity;
using TjommeMetSomme.Resources;
using TjommeMetSomme.Services;
using TjommeMetSomme.Validators;

namespace TjommeMetSomme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IStudentService _studentService;

        private readonly IMapper _mapper;
        
        public StudentsController(UserManager<ApplicationUser> userManager, IStudentService studentService, IMapper mapper)
        {
            _userManager = userManager;

            _mapper = mapper;

            _studentService = studentService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<StudentResource>>> GetAll(int parentId = 0, bool includeParent = true)
        {

            IEnumerable<Student> students;
            
            if (!parentId.Equals(0))
            {
                students = await _studentService.GetAllByParentId(parentId, includeParent);
            } else
            {
                students = await _studentService.GetAll(includeParent);
            }

            var studentResources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return Ok(studentResources);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StudentResource>> GetById(int id, bool includeParent = true)
        {
            var student = await _studentService.GetById(id, includeParent);

            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<StudentResource>> CreateStudent([FromBody] CreateStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveStudentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var user = _mapper.Map<CreateStudentResource, ApplicationUser>(saveStudentResource);

            var userCreateResult = await _userManager.CreateAsync(user, saveStudentResource.Password);

            if (!userCreateResult.Succeeded)
            {
                return Problem(userCreateResult.Errors.First().Description, null, 500);
            }

            var userAddToRoleResult = await _userManager.AddToRoleAsync(user, Constants.Student.Role.NAME);

            if (!userAddToRoleResult.Succeeded)
            {
                return Problem(userAddToRoleResult.Errors.First().Description, null, 500);
            }

            var studentToCreate = _mapper.Map<CreateStudentResource, Student>(saveStudentResource);

            studentToCreate.ApplicationUserId = user.Id;
            studentToCreate.ApplicationRoleId = Constants.Student.Role.ID;

            var newStudent = await _studentService.Create(studentToCreate);

            var student = await _studentService.GetById(newStudent.Id, true);

            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<StudentResource>> UpdateStudent(int id, [FromBody] CreateStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveStudentResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var studentToBeUpdated = await _studentService.GetById(id, true);

            if (studentToBeUpdated == null)
            {
                return NotFound();
            }

            var student = _mapper.Map<CreateStudentResource, Student>(saveStudentResource);

            await _studentService.Update(studentToBeUpdated, student);

            var updatedStudent = await _studentService.GetById(id, true);

            var updatedStudentResource = _mapper.Map<Student, StudentResource>(updatedStudent);

            return Ok(updatedStudentResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var student = await _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            await _studentService.Delete(student);

            return NoContent();
        }
    }
}
