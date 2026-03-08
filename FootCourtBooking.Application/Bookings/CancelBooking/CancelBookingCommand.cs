using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootCourtBooking.Application.Bookings.CancelBooking
{
    public sealed record CancelBookingCommand
    (
        Guid BookingId
    );
        
    
}