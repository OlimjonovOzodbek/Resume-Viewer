using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Resume.Application.Abstractions;
using Resume.Application.UseCases.Resume.Queries;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Handlers.QueryHandlers
{
    public class GetResumeByUserIdQueryHandler : IRequestHandler<GetResumeByUserIdQuery, ResumeModel>
    {
        private readonly IResumeDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetResumeByUserIdQueryHandler(IResumeDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResumeModel> Handle(GetResumeByUserIdQuery request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={request.UserId}");

                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                    var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Id == request.ResumeId);

                    return resume;
                }
                else
                {
                    return new ResumeModel();
                }
            }
        }
    }
}
