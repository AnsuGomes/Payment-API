using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment_API.Enums
{
    public enum StatusTransitions
    {
        Awaiting_Payment = 0,
        PaymentAccept = 1,
        SentToCarrier = 2,
        Delivered = 3,
        Canceled = 4
    }
}