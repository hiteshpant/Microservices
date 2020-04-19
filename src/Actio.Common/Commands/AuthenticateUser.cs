using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Commands
{
    public class AuthenticateUser:IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
