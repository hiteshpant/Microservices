using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public abstract class IRejectedEvent : IntegrationEvent
    {
        public abstract string Reason { get; }
        public abstract string Code { get; }
    }
}
