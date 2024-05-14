using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public ResumeController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<UserModel> GetByUserId(Guid userId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={userId}");

            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                var res = await _mediatr.Send(new GetResumeByUserIdQuery { UserId = userId });
                
                return user;
            }

            return new UserModel();
        }
    }
}
