using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Domain.Bookings;
using FootCourtBooking.Domain.Common;

namespace FootCourtBooking.Application.Bookings.ConfirmBooking
{
    public class ConfirmBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public ConfirmBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> Handle(ConfirmBookingCommand confirmBookingCommand, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(confirmBookingCommand.BookingId, cancellationToken);

            if (booking is null)
                throw new NotFoundException("Booking not found.");

            booking.Confirm();

            await _bookingRepository.UpdateAsync(booking, cancellationToken);

            return new BookingDto(
                booking.Id,
                booking.CourtId,
                booking.CustomerName,
                booking.Slot.Start,
                booking.Slot.End,
                booking.Status.ToString(),
                booking.CreatedAtUtc);
        }

    }
}