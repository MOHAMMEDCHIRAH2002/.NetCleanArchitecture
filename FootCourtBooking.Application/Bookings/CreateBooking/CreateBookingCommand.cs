using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootCourtBooking.Application.Bookings.CreateBooking
{
    public sealed record CreateBookingCommand
    (
        Guid CourtId,
        string CustomerName,
        DateTime StartUtc
        
    );
}