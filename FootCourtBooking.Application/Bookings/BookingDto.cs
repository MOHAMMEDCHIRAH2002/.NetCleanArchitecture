using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootCourtBooking.Application.Bookings
{
    public sealed record BookingDto
    (
        Guid Id,
        Guid CourtId,
        string CustomerName,
        DateTime StartUtc,
        DateTime EndUtc,
        string Status,
        DateTime CreatedAtUtc
    );
        
    
}