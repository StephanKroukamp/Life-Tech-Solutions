﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Resources;
using TjommeMetSomme.Services;
using TjommeMetSomme.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TjommeMetSomme.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace TjommeMetSomme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

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
        public async Task<ActionResult<IEnumerable<StudentResource>>> GetAllStudents()
        {
            var students = await _studentService.GetAllWithParent();

            var studentResources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return Ok(studentResources);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StudentResource>> GetStudentByIdWithParent(int id)
        {
            var student = await _studentService.GetStudentByIdWithParent(id);

            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<StudentResource>> CreateStudent([FromBody] SaveStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveStudentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var user = _mapper.Map<SaveStudentResource, ApplicationUser>(saveStudentResource);

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

            var studentToCreate = _mapper.Map<SaveStudentResource, Student>(saveStudentResource);

            var newStudent = await _studentService.CreateStudent(studentToCreate);

            var student = await _studentService.GetStudentByIdWithParent(newStudent.Id);

            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<StudentResource>> UpdateStudent(int id, [FromBody] SaveStudentResource saveStudentResource)
        {
            var validator = new SaveStudentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveStudentResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var studentToBeUpdated = await _studentService.GetStudentByIdWithParent(id);

            if (studentToBeUpdated == null)
            {
                return NotFound();
            }

            var student = _mapper.Map<SaveStudentResource, Student>(saveStudentResource);

            await _studentService.UpdateStudent(studentToBeUpdated, student);

            var updatedStudent = await _studentService.GetStudentByIdWithParent(id);

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

            var student = await _studentService.GetStudentByIdWithParent(id);

            if (student == null)
            {
                return NotFound();
            }

            await _studentService.DeleteStudent(student);

            return NoContent();
        }
    }
}
