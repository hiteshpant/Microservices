using System;
using System.Threading;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using EventBus.Abstratction;
using MediatR;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserService _UserService;
        private readonly IEventBus _BusClient;
        private readonly ILogger<CreateUserCommand> _logger;

        public CreateUserHandler(IEventBus busClient, IUserService userservice, ILogger<CreateUserCommand> logger)
        {
            _UserService = userservice;
            _BusClient = busClient;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with name: '{command.Name}'.");
            try
            {
                await _UserService.RegisterAsync(command.Email, command.Password, command.Name);           
                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.Name}'.");
                             
            }
            catch (ActioException ex)
            {
                _logger.LogError(ex, ex.Message);
                 _BusClient.Publish(new CreatedUserRejected(command.Email,
                    ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                 _BusClient.Publish(new CreatedUserRejected(command.Email,
                    ex.Message, "error"));
            }
            return Unit.Value;
        }
    }
}