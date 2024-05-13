using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities.Models;

namespace User.Application.UseCases.User.Commands
{
    public class DeleteUserCommand: IRequest<ResponceModel>
    {
        public Guid Id { get; set; }
    }
}
