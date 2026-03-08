using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Domain.Common;

namespace FootCourtBooking.Application.Bookings.GetBookingById
{
    public class GetBookingByIdHandler
    {
        private readonly IBookingRepository   _bookingRepository;

        public GetBookingByIdHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> Handle(GetBookingByIdQuery query,CancellationToken cancellationToken=default)
        {
            var booking=await _bookingRepository.GetByIdAsync(query.Id, cancellationToken);

            if(booking is null)
               throw new NotFoundException("Booking not found.");

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