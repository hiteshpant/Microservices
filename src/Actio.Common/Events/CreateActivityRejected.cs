using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public override string Reason { get; }
        public override string Code { get; }

        protected CreateActivityRejected()
        {

        }

        public CreateActivityRejected(Guid id,
            string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}
