using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Resume.Application.UseCases.Resume.Commands;
using Resume.Application.UseCases.Resume.Queries;
using Resume.Domain.Entities.Models;
using System.Net.Http;

namespace Resume.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ResumeController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        [Authorize(Roles = "User,SuperAdmin")]
        public async Task<ResponseModel> CreateResume(CreateResumeCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }

        [HttpPost]
        [Authorize(Roles = "User,SuperAdmin")]
        public async Task<ResponseModel> UploadResume(UploadResumeCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }

        [HttpPatch]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ResponseModel> SetStatus(SetStatusCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }

        [HttpGet]
        [Authorize(Roles = "User,SuperAdmin")]
        public async Task<List<ResumeModel>> GetAllResumeByUserId(Guid UserId, string Token)
        {
            var res = await _mediatr.Send(new GetAllResumeByUserIdQuery { UserId = UserId, Token = Token });

            return res;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<List<ResumeModel>> GetAllResume()
        {
            var res = await _mediatr.Send(new GetAllResumeQuery());

            return res;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin, SuperAdmin")]
        public async Task<ResumeModel> GetById(Guid id)
        {
            var res = await _mediatr.Send(new GetResumeByIdQuery { ResumeId = id });

            return res;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin, User")]
        public async Task<ResumeModel> GetByUserId(Guid userId, Guid resumeId, string Token)
        {
            var res = await _mediatr.Send(new GetResumeByUserIdQuery { UserId = userId, ResumeId = resumeId, Token = Token });

            return res;
        }

        [HttpDelete]
        [Authorize(Roles = "User,SuperAdmin")]
        public async Task<ResponseModel> Delete(DeleteResumeCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }
    }
}
