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
    public class ParentsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IParentService _parentService;

        private readonly IMapper _mapper;

        public ParentsController(UserManager<ApplicationUser> userManager, IParentService parentService, IMapper mapper)
        {
            _userManager = userManager;

            _parentService = parentService;

            _mapper = mapper;
        }

        [HttpGet("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<IEnumerable<ParentResource>>> GetAll(bool includeStudents = true)
        {
            var parents = await _parentService.GetAll(includeStudents);

            var parentResponses = _mapper.Map<IEnumerable<Parent>, IEnumerable<ParentResource>>(parents);

            return Ok(parentResponses);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> GetById(int id, bool includeStudents = true)
        {
            var parent = await _parentService.GetById(id, includeStudents);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> Create([FromBody] CreateParentResource createParentResource)
        {
            var validator = new CreateParentResourceValidator();

            var validationResult = await validator.ValidateAsync(createParentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var user = _mapper.Map<CreateParentResource, ApplicationUser>(createParentResource);

            var userCreateResult = await _userManager.CreateAsync(user, createParentResource.Password);

            if (!userCreateResult.Succeeded)
            {
                return Problem(userCreateResult.Errors.First().Description, null, 500);
            }

            var userAddToRoleResult = await _userManager.AddToRoleAsync(user, Constants.Parent.Role.NAME);

            if (!userAddToRoleResult.Succeeded)
            {
                return Problem(userAddToRoleResult.Errors.First().Description, null, 500);
            }

            var parentToCreate = _mapper.Map<CreateParentResource, Parent>(createParentResource);

            parentToCreate.ApplicationUserId = user.Id;
            parentToCreate.ApplicationRoleId = Constants.Parent.Role.ID;

            var newParent = await _parentService.Create(parentToCreate);

            var parent = await _parentService.GetById(newParent.Id, false);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }

        //TODO: Ensure that the updating of email and username is unique and would throw an error if trying to update it to another email that already exists in the system
        //TODO: Perhaps move the updating of identityUser data to auth controller
        //TODO: Add a change password method to the auth controller that allows for password to be changed
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> Update(int id, [FromBody] UpdateParentResource updateParentResource)
        {
            var validator = new UpdateParentResourceValidator();

            var validationResult = await validator.ValidateAsync(updateParentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var parentToBeUpdated = await _parentService.GetById(id);

            if (parentToBeUpdated == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(parentToBeUpdated.ApplicationUserId.ToString());

            if (user == null)
            {
                return NotFound();

            }
            user.Email = updateParentResource.Email;
            user.UserName = updateParentResource.UserName;
            user.FirstName = updateParentResource.FirstName;
            user.LastName = updateParentResource.LastName;

            var userUpdateResult = await _userManager.UpdateAsync(user);

            if (!userUpdateResult.Succeeded)
            {
                return Problem(userUpdateResult.Errors.First().Description, null, 500);
            }

            var parent = await _parentService.GetById(id, false);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }
            
        //TODO: Test delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var parent = await _parentService.GetById(id);

            await _parentService.Delete(parent);

            return NoContent();
        }


    }
}