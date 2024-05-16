using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetRateResumeQueryHandler : IRequestHandler<GetRateResumeQuery, ResponseModel>
    {
        private readonly IResumeDbContext _context;

        public GetRateResumeQueryHandler(IResumeDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(GetRateResumeQuery request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == request.ResumeId);

            if (resume == null)
            {
                return new ResponseModel
                {
                    Message = "Resume not found",
                    Status = 404
                };
            }

            var technologies = request.Keywords.Split(' ').ToList();

            var text = PdfRead(resume.Document).ToLower();

            int count = 0;

            foreach (var item in technologies)
            {
                if (text.Contains(item.ToLower(), StringComparison.OrdinalIgnoreCase))
                {
                    count += 1;
                }
            }

            if (count == technologies.Count)
            {
                return new ResponseModel
                {
                    Message = "100",
                    Status = 200,
                    isSuccess = true
                };
            }
            else if (count == 0)
            {
                return new ResponseModel
                {
                    Message = "0",
                    Status = 200,
                    isSuccess = true
                };
            }
            else
            {
                double rate =  (double)count / technologies.Count * 100;
                return new ResponseModel
                {
                    Message = $"{rate}",
                    Status = 200,
                    isSuccess = true
                };
            }
        }

        private string PdfRead(string filePath)
        {
            PdfReader pdfReader = new PdfReader(filePath);
            PdfDocument pdfDocument = new PdfDocument(pdfReader);

            string res = "";

            for (int i = 1; i < pdfDocument.GetNumberOfPages(); i++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                string data = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), strategy);
                res += data;
            }

            return res;
        }
    }
}
