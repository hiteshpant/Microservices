using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EventBus.Abstratction;

namespace EventBus
{
    public class InMemorySubscriptionManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<String, List<SubscriptionInfo>> _Handlers;
        private readonly List<Type> _eventTypes;
        public bool IsEmpty => _Handlers.Any();

        public event EventHandler<string> OnEventRemoved;

        public InMemorySubscriptionManager()
        {
            _Handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (_Handlers.Count == 0)
            {
                _Handlers[eventName] = new List<SubscriptionInfo> { SubscriptionInfo.Typed(handlerType) };
            }
            else if (_Handlers[eventName].Contains(SubscriptionInfo.Typed(typeof(TH))))
            {
                throw new ArgumentException(
                 $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }
            else
            {
                _Handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }


            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        public void Clear()
        {
            _Handlers.Clear();
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes?.SingleOrDefault(x => x.Name == eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            return _Handlers[typeof(T).Name];
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _Handlers.Count > 0 ? _Handlers[eventName] : null;
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            return GetHandlersForEvent<T>().Any();
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            return GetHandlersForEvent(eventName)?.FirstOrDefault() != null;
        }

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var Handler = typeof(TH);
            var handlerToBeRemoved = _Handlers[eventName]?.SingleOrDefault(subsription => subsription.HandlerType == Handler);
            _Handlers[eventName].Remove(handlerToBeRemoved);
        }
    }
}
