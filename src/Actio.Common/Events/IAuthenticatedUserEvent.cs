using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public abstract class IAuthenticatedUserEvent:IntegrationEvent
    {
        Guid UserId { get; }
    }
}
