using System;
using System.Threading.Tasks;
using Actio.API.Model;
using Actio.Common.Commands;
using Actio.Common.Events;
using EventBus.Abstratction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.API.Controllers
{
    [Route("api/[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IEventBus _eventBus;

        public ActivitiesController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Activity activity)
        {
            activity.Id = Guid.NewGuid();
            activity.UserId = Guid.NewGuid();
            activity.CreatedAt = activity.CreatedAt;
            activity.Name = activity.Name;
            activity.Category = activity.Category;

            var activityEvent = new ActivityCreated(activity.Id, activity.UserId, activity.Category, activity.Name,
                activity.Description, activity.CreatedAt);
         
            //Publish the ActivityCreated Event via event bus
            //Actio.Services.Actvities will subscribe to this and process it using command handler
            _eventBus.Publish(activityEvent);

            return Accepted($"actvities/{activity.Id}");
        }

    }
}
