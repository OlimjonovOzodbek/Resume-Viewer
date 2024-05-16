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
        public async Task<List<ResumeModel>> GetAllResumeByUserId(Guid UserId, string Token, int PageIndex, int Size)
        {
            var res = await _mediatr.Send(new GetAllResumeByUserIdQuery { UserId = UserId, Token = Token, PageIndex = PageIndex, Size = Size });

            return res;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<List<ResumeModel>> GetAllResume(int PageIndex, int Size)
        {
            var res = await _mediatr.Send(new GetAllResumeQuery() { PageIndex = PageIndex, Size = Size });

            return res;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin, SuperAdmin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediatr.Send(new GetResumeByIdQuery { ResumeId = id });

            if (result is FileStreamResult fileResult)
            {
                return fileResult;
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin, User")]
        public async Task<IActionResult> GetByUserId(Guid userId, Guid resumeId, string Token)
        {
            var result = await _mediatr.Send(new GetResumeByUserIdQuery { UserId = userId, ResumeId = resumeId, Token = Token });

            if (result is FileStreamResult fileResult)
            {
                return fileResult;
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ResponseModel> GetResumeRate(Guid resumeId, string keywords)
        {
            var result = await _mediatr.Send(new GetRateResumeQuery { ResumeId = resumeId, Keywords = keywords });

            return result;
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
