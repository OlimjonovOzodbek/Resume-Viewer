using MediatR;
using Newtonsoft.Json;
using Resume.Application.Abstractions;
using Resume.Application.UseCases.Resume.Commands;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Handlers.CommandHandlers
{
    public class UploadResumeCommandHandler : IRequestHandler<UploadResumeCommand, ResponseModel>
    {
        private readonly IResumeDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public UploadResumeCommandHandler(IResumeDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> Handle(UploadResumeCommand request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={request.UserId}");

            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                var resume = new ResumeModel
                {
                    UserId = user.Id,
                    Name = request.Document,
                    Document = request.Document
                };

                await _context.Resumes.AddAsync(resume);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    Message = "Resume uploaded!",
                    Status = 201,
                    isSuccess = true
                };
            }

            return new ResponseModel
            {
                Message = "User not found!",
                Status = 404
            };
        }
    }
}
