using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Abstractions;
using Resume.Application.UseCases.Resume.Commands;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Resume.Application.UseCases.Resume.Handlers.CommandHandlers
{
    public class SetStatusCommandHandler : IRequestHandler<SetStatusCommand, ResponseModel>
    {
        private readonly IResumeDbContext _context;
        private readonly IConfiguration _config;

        public SetStatusCommandHandler(IResumeDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ResponseModel> Handle(SetStatusCommand request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken);

            if (resume != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);

                    HttpResponseMessage response = await client.GetAsync($"https://localhost:7264/api/User/GetById?id={resume.UserId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var user = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                        var emailSettings = _config.GetSection("EmailSettings");

                        var mailMessage = new MailMessage();

                        if (request.Score <= 50)
                        {
                            mailMessage.From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]);
                            mailMessage.Subject = "Rejection";
                            mailMessage.Body = "Unfortunataly you got rejected!";
                            mailMessage.IsBodyHtml = true;
                        }
                        else if (request.Score > 50)
                        {
                            mailMessage.From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]);
                            mailMessage.Subject = "Acceptance";
                            mailMessage.Body = "Congratulations! Your resume got accepted.";
                            mailMessage.IsBodyHtml = true;
                        }
                        mailMessage.To.Add(user.Email);

                        using var smtpClient = new SmtpClient(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]))
                        {
                            Port = Convert.ToInt32(emailSettings["MailPort"]),
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
                            EnableSsl = true,
                        };

                        await smtpClient.SendMailAsync(mailMessage);
                        resume.Status = request.Status;

                        _context.Resumes.Update(resume);
                        await _context.SaveChangesAsync(cancellationToken);

                        return new ResponseModel
                        {
                            Message = "Status updated!",
                            Status = 200,
                            isSuccess = true
                        };
                    }
                    return new ResponseModel
                    {
                        Message = "User not found",
                        Status = 404
                    };
                }
            }
            return new ResponseModel
            {
                Message = "Resume not found!",
                Status = 404
            };
        }
    }
}
