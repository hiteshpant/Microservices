using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class UserAuthenticated : IntegrationEvent
    {
        public string Email { get; }

        public UserAuthenticated(string email)
        {
            Email = email;
        }

        protected UserAuthenticated()
        {
                
        }
    }
}
