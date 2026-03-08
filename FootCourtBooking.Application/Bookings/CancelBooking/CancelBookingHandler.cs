using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Domain.Common;

namespace FootCourtBooking.Application.Bookings.CancelBooking
{
    public class CancelBookingHandler
    {
        
        private readonly IBookingRepository _bookingRepository;

        public CancelBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }


        public async Task<BookingDto> Handle(CancelBookingCommand cancelBookingCommand,CancellationToken cancellationToken = default)
        {
            
            var booking=await _bookingRepository.GetByIdAsync(cancelBookingCommand.BookingId, cancellationToken);

            if(booking is null)
                throw new NotFoundException("Booking not found.");

                booking.Cancel(DateTime.UtcNow);

                await _bookingRepository.UpdateAsync(booking, cancellationToken);

                return new BookingDto(
                booking.Id,
                booking.CourtId,
                booking.CustomerName,
                booking.Slot.Start,
                booking.Slot.End,
                booking.Status.ToString(),
                booking.CreatedAtUtc
                );


        }
    }
}