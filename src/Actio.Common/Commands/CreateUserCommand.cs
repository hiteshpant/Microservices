using MediatR;

namespace Actio.Common.Commands
{
   public  class CreateUserCommand: IRequest
    {
        public string Email { get; set; }
        public string Name{ get; set; }
        public string Password { get; set; }
               
    }
}
