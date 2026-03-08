using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootCourtBooking.Application.Bookings.ConfirmBooking
{
    public sealed record ConfirmBookingCommand(Guid BookingId);
        
    
}