using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.UseCases.User.Commands;
using User.Application.UseCases.User.Queries;
using User.Domain.Entities.Models;

namespace User.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<UserModel> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUsersByIdQuery()
            {
                Id = id
            });

            return result;
        }

        [HttpPatch]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ResponceModel> SetRole(SetRoleCommand request)
        {
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<ResponceModel> Update(UpdateUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<ResponceModel> Delete(DeleteUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result;
        }
    }
}
