using EventBus.Abstratction;
using Newtonsoft.Json;
using System;

namespace EventBus
{
    public class IntegrationEvent
    {

        public IntegrationEvent()
        {

        }        
        [JsonConstructor]
        public IntegrationEvent(Guid id,DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

        public Guid Id{ get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
