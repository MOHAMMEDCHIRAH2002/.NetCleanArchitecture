using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootCourtBooking.Api.Contracts
{
    public sealed record CreateBookingRequest
    (
        Guid CourtId ,
        string CustomerName,
        DateTime StartUtc
        
    );
}