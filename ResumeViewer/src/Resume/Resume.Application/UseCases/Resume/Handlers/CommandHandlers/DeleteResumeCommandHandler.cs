using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand, ResponseModel>
    {
        private readonly IResumeDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteResumeCommandHandler(IResumeDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseModel> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={request.UserId}");

                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                    var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Id == request.ResumeId);

                    if (resume == null)
                    {
                        return new ResponseModel
                        {
                            Message = "Resume not found!",
                            Status = 404
                        };
                    }
                    _context.Resumes.Remove(resume);
                    await _context.SaveChangesAsync(cancellationToken);

                    return new ResponseModel
                    {
                        Message = "Resume removed!",
                        Status = 200,
                        isSuccess = true
                    };
                }

                else
                {
                    return new ResponseModel
                    {
                        Message = "User not found!",
                        Status = 404
                    };
                }
            }
        }
    }
}
