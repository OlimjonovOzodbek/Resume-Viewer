using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities.Models;

namespace Resume.Domain.Entities.Models
{
    public class ResumeModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Status { get; set; } = "none";
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

    }
}
