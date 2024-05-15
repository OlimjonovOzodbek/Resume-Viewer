using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities.Models;

namespace User.Application.UseCases.User.Queries
{
    public class GetAllUsersQuery: IRequest<IEnumerable<UserModel>>
    {
        public int PageIndex { get; set; }
        public int Size { get; set; }
    }
}
