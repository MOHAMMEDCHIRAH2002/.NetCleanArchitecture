using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Domain.Bookings;

namespace FootCourtBooking.Application.Abstractions.Persistence
{
    public interface IBookingRepository
    {

        Task AddAsync (Booking booking, CancellationToken cancellationToken=default);

        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);

        Task<IReadOnlyList<Booking>> GetByCourtIdAsync(Guid courtId, CancellationToken cancellationToken=default);

         Task UpdateAsync(Booking booking, CancellationToken cancellationToken=default);

         Task<bool> ExistsOverlappingAsync(Guid courtId,TimeSlot slot, CancellationToken cancellationToken=default);
        
    }
}