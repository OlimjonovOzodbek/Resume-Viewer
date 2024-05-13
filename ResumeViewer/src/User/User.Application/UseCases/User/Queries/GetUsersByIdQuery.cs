using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities.Models;

namespace User.Application.UseCases.User.Queries
{
    public class GetUsersByIdQuery: IRequest<UserModel>
    {
        public Guid Id { get; set; }
    }
}
