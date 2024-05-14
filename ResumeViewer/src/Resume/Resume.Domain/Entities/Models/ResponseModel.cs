using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Domain.Entities.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public bool isSuccess { get; set; } = false;
    }
}
