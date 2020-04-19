using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using EventBus.Abstratction;
using MediatR;

namespace Actio.Services.Activities
{
    internal class ActivityCreatedIntegrationEventHandler : IIntegrationEventHandler<ActivityCreated>
    {
        private readonly IMediator _mediator;

        public ActivityCreatedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Handle(ActivityCreated @event)
        {
            var command = new CreateActivityCommand();
            command.Category = @event.Category;
            command.UserId = @event.UserId;
            command.Id = @event.Id;
            command.Name = @event.Name;
            command.CreatedAt = @event.CreatedAt;
            return _mediator.Send(command);
        }
    }
}