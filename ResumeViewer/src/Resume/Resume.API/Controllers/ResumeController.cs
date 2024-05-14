using MediatR;
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
    public class ResumeController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ResumeController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ResponseModel> CreateResume(CreateResumeCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }

        [HttpPost]
        public async Task<ResponseModel> UploadResume(UploadResumeCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }

        [HttpPatch]
        public async Task<ResponseModel> SetStatus(SetStatusCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }

        [HttpGet]
        public async Task<List<ResumeModel>> GetAllResumeByUserId(Guid UserId)
        {
            var res = await _mediatr.Send(new GetAllResumeByUserIdQuery { UserId = UserId });

            return res;
        }

        [HttpGet]
        public async Task<List<ResumeModel>> GetAllResume()
        {
            var res = await _mediatr.Send(new GetAllResumeQuery());

            return res;
        }

        [HttpGet]
        public async Task<ResumeModel> GetById(Guid id)
        {
            var res = await _mediatr.Send(new GetResumeByIdQuery { ResumeId = id });

            return res;
        }

        [HttpGet]
        public async Task<ResumeModel> GetByUserId(Guid userId, Guid resumeId)
        {
            var res = await _mediatr.Send(new GetResumeByUserIdQuery { UserId = userId, ResumeId = resumeId });

            return res;
        }

        [HttpDelete]
        public async Task<ResponseModel> Delete(DeleteResumeCommand request)
        {
            var res = await _mediatr.Send(request);

            return res;
        }
    }
}
