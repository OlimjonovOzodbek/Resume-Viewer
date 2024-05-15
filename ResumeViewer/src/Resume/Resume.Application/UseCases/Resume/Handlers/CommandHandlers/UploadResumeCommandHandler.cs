using MediatR;
using Microsoft.AspNetCore.Hosting;
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
    public class UploadResumeCommandHandler : IRequestHandler<UploadResumeCommand, ResponseModel>
    {
        private readonly IResumeDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadResumeCommandHandler(IResumeDbContext context, IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(UploadResumeCommand request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={request.UserId}");

                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                    string fileName = "";
                    string filePath = "";

                    if (request.Document is not null)
                    {
                        var file = request.Document;

                        if (file.ContentType != "application/pdf" || Path.GetExtension(file.FileName).ToLower() != ".pdf")
                        {
                            return new ResponseModel()
                            {
                                Message = "Invalid file type. Only PDF files are allowed.",
                                Status = 400
                            };
                        }

                        try
                        {
                            fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            filePath = Path.Combine(_webHostEnvironment.WebRootPath, "PDFs", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var resume = new ResumeModel
                            {
                                UserId = user.Id,
                                Name = request.Document.Name,
                                Document = filePath
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
                        catch (Exception ex)
                        {
                            return new ResponseModel()
                            {
                                Message = ex.Message,
                                Status = 400
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel()
                        {
                            Message = "No file provided.",
                            Status = 400
                        };
                    }
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
