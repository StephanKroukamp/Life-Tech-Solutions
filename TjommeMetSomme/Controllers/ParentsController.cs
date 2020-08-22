using System.Collections.Generic;
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
    public class ParentsController : ControllerBase
    {
        private readonly IParentService _parentService;

        private readonly IMapper _mapper;

        public ParentsController(IParentService parentService, IMapper mapper)
        {
            _mapper = mapper;

            _parentService = parentService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ParentResource>>> GetAllParents()
        {
            var parents = await _parentService.GetAllParents();

            var parentResponses = _mapper.Map<IEnumerable<Parent>, IEnumerable<ParentResource>>(parents);

            return Ok(parentResponses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ParentResource>> GetParentById(int id)
        {
            var parent = await _parentService.GetParentById(id);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> CreateParent([FromBody] SaveParentResource saveParentResource)
        {
            var validator = new SaveParentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveParentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var parentToCreate = _mapper.Map<SaveParentResource, Parent>(saveParentResource);

            var newParent = await _parentService.CreateParent(parentToCreate);

            var parent = await _parentService.GetParentById(newParent.ParentId);

            var parentResource = _mapper.Map<Parent, ParentResource>(parent);

            return Ok(parentResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<ActionResult<ParentResource>> UpdatParent(int id, [FromBody] SaveParentResource saveParentResource)
        {
            var validator = new SaveParentResourceValidator();

            var validationResult = await validator.ValidateAsync(saveParentResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // this needs refining
            }

            var parentToBeUpdated = await _parentService.GetParentById(id);

            if (parentToBeUpdated == null)
            {
                return NotFound();
            }

            var parent = _mapper.Map<SaveParentResource, Parent>(saveParentResource);

            await _parentService.UpdateParent(parentToBeUpdated, parent);

            var updatedParent = await _parentService.GetParentById(id);

            var updatedParentResource = _mapper.Map<Parent, ParentResource>(updatedParent);

            return Ok(updatedParentResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var parent = await _parentService.GetParentById(id);

            await _parentService.DeleteParent(parent);

            return NoContent();
        }
    }
}