using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Resume.Application.Abstractions;
using Resume.Application.UseCases.Resume.Queries;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Handlers.QueryHandlers
{
    public class GetAllResumeByUserIdQueryHandler : IRequestHandler<GetAllResumeByUserIdQuery, List<ResumeModel>>
    {
        private readonly IResumeDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetAllResumeByUserIdQueryHandler(IResumeDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResumeModel>> Handle(GetAllResumeByUserIdQuery request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

            var response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={request.UserId}");

            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync(cancellationToken));

                var userResumes = await _context.Resumes.Where(r => r.UserId == request.UserId).ToListAsync(cancellationToken);

                return userResumes;
            }

            return [];
        }
    }
}
