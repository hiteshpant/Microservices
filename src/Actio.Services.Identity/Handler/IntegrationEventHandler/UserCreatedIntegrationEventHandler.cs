using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Services.Identity;
using EventBus.Abstratction;
using MediatR;

namespace Actio.Services.Activities
{
    internal class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreated>
    {
        private readonly IMediator _mediator;

        public UserCreatedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Handle(UserCreated @event)
        {
            var command = new CreateUserCommand();
            command.Email = @event.Email;
            command.Name = @event.Name;
            command.Password = @event.Password;
            return _mediator.Send(command);
        }
    }
}