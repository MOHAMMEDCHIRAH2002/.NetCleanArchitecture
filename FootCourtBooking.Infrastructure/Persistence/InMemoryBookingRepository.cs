using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Domain.Bookings;

namespace FootCourtBooking.Infrastructure.Persistence
{
    public sealed class InMemoryBookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings = [];
        private readonly object _lock = new();
        public Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                _bookings.Add(booking);
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsOverlappingAsync(Guid courtId, TimeSlot slot, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                return Task.FromResult(_bookings.Any(b => b.CourtId == courtId && b.Slot.Overlaps(slot)));
            }
        }

        public Task<IReadOnlyList<Booking>> GetByCourtIdAsync(Guid courtId, CancellationToken cancellationToken = default)
        {
           IReadOnlyList<Booking> bookings;
            lock (_lock)
            {
                bookings = _bookings.Where(b => b.CourtId == courtId).ToList();
            }
            return Task.FromResult(bookings);
        }

        public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Booking? booking;
            lock (_lock)
            {
                booking = _bookings.FirstOrDefault(b => b.Id == id);
            }
            return Task.FromResult(booking);
        }

        public Task UpdateAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var index = _bookings.FindIndex(b => b.Id == booking.Id);
                if (index != -1)
                {
                    _bookings[index] = booking;
                }
            }
            return Task.CompletedTask;
        }
    }
}