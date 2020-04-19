using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class CreatedUserRejected : IRejectedEvent
    {
        public string Email { get; }

        public override string Reason { get; }

        public override string Code { get; }

        public CreatedUserRejected(string email, string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }

        public CreatedUserRejected()
        {

        }
    }
}