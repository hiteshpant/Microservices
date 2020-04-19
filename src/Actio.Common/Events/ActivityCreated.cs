using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class ActivityCreated : IntegrationEvent
    {
        public Guid UserId { get; }
        public Guid Id { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

        public ActivityCreated(Guid id, Guid userId, string category,string name,string description, DateTime createdAt)
        {
            Id = id;
            UserId= userId;
            Category = category;
            Name = name;
            CreatedAt = createdAt;
            description = Description;
        }

        protected ActivityCreated()
        {

        }

    }
}
