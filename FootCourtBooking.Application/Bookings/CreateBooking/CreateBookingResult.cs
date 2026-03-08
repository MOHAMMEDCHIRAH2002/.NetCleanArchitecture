using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootCourtBooking.Application.Bookings.CreateBooking
{
    public sealed record CreateBookingResult
    (
        Guid Id,
        Guid CourtId,
        string CustomerName,
        DateTime StartUtc,
        DateTime EndUtc,
        string Status
    );
}