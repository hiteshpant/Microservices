using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class UserCreated: IntegrationEvent
    {
        public string Email { get; set; }

        public string Name{ get; set; }

        public string Password { get; set; }

        public UserCreated()
        {

        }

        public UserCreated(string name, string email,string password)
        {
            Email = email;
            Name = name;
            Password = password;
        }
    }
}
