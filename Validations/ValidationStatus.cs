using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment_API.Enums;

namespace Payment_API.Validations
{
    public class ValidationStatus
    {
        public static bool ValidationStatusCurrent(StatusTransitions currentStats, StatusTransitions newStatus)
        {
            switch(currentStats)
            {
                case StatusTransitions.Awaiting_Payment:
                    return (newStatus == StatusTransitions.PaymentAccept || newStatus == StatusTransitions.Canceled) ? true : false;
                
                case StatusTransitions.PaymentAccept:
                    return (newStatus == StatusTransitions.SentToCarrier || newStatus == StatusTransitions.Canceled) ? true : false;

                case StatusTransitions.SentToCarrier:
                    return (newStatus == StatusTransitions.Delivered) ? true : false;

                default:
                    return false;
            }
        }
    }
}