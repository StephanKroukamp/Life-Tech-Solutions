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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ParentResource>>> GetAll(bool includeStudents = true)
        {
            var parents = await _parentService.GetAll(includeStudents, true);

            var parentResponses = _mapper.Map<IEnumerable<Parent>, IEnumerable<ParentResource>>(parents);

            return Ok(parentResponses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ParentResource>> GetById(int id, bool includeStudents = true)
        {
            var parent = await _parentService.GetById(id, includeStudents, true);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> Create([FromBody] SaveParentResource saveParentResource)
        {
            var validator = new SaveParentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveParentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var user = _mapper.Map<SaveParentResource, ApplicationUser>(saveParentResource);

            var userCreateResult = await _userManager.CreateAsync(user, saveParentResource.Password);

            if (!userCreateResult.Succeeded)
            {
                return Problem(userCreateResult.Errors.First().Description, null, 500);
            }

            var userAddToRoleResult = await _userManager.AddToRoleAsync(user, Constants.Parent.Role.NAME);

            if (!userAddToRoleResult.Succeeded)
            {
                return Problem(userAddToRoleResult.Errors.First().Description, null, 500);
            }

            var parentToCreate = _mapper.Map<SaveParentResource, Parent>(saveParentResource);

            parentToCreate.ApplicationUserId = user.Id;

            var newParent = await _parentService.Create(parentToCreate);

            var parent = await _parentService.GetById(newParent.Id);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> Update(int id, [FromBody] SaveParentResource saveParentResource)
        {
            var validator = new SaveParentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveParentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var parentToBeUpdated = await _parentService.GetById(id);

            if (parentToBeUpdated == null)
            {
                return NotFound();
            }

            var parent = _mapper.Map<SaveParentResource, Parent>(saveParentResource);

            await _parentService.Update(parentToBeUpdated, parent);

            var updatedParent = await _parentService.GetById(id);

            var updatedParentResource = _mapper.Map<Parent, ParentResource>(updatedParent);

            return Ok(updatedParentResource);
        }

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