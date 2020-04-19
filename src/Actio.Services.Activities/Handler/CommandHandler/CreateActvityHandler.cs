using System;
using System.Threading;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using EventBus.Abstratction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Activities
{
    public class CreateActvityCommandHandler : IRequestHandler<CreateActivityCommand, bool>
    {
        private readonly ILogger _logger;
        private readonly IEventBus _busClient;
        private readonly IActivityService _activityService;

        public CreateActvityCommandHandler(IEventBus busClient,
            IActivityService activityService,
            ILogger<CreateActvityCommandHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateActivityCommand command, CancellationToken cancellationToken)
        {
            bool result = false;
            _logger.LogInformation($"Creating activity: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name,
                    command.Description, command.CreatedAt);             
                
                result = true;
                _logger.LogInformation($"Activity: '{command.Id}' was created for user: '{command.UserId}'.");

            }
            catch (ActioException ex)
            {
                _logger.LogError(ex, ex.Message);
                ///Publish CreateActivityRejected Event through Rabbit MQ Bus
                _busClient.Publish(new CreateActivityRejected(command.Id,
                   ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ///Publish CreateActivityRejected Event through Rabbit MQ Bus
                _busClient.Publish(new CreateActivityRejected(command.Id,
                    ex.Message, "error"));
                result = false;

            }
            return result;
        }
    }
}