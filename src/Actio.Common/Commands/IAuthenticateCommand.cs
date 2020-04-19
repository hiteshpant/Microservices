using MediatR;
using System;


namespace Actio.Common.Commands
{
    public interface IAuthenticateCommand: IRequest
    {
         Guid UserId { get; set; }
    }
}
