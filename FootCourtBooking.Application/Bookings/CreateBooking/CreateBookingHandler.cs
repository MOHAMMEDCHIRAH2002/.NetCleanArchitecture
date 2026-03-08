using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Domain.Bookings;
using FootCourtBooking.Domain.Common;

namespace FootCourtBooking.Application.Bookings.CreateBooking
{
    public class CreateBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public CreateBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<CreateBookingResult> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            if (command.StartUtc.Kind != DateTimeKind.Utc)
                throw new DomainException("StartUtc must be in UTC");

            var slot = new TimeSlot(command.StartUtc, command.StartUtc.AddHours(1));

            var alreadyBooked = await _bookingRepository.ExistsOverlappingAsync(command.CourtId, slot, cancellationToken);

            if (alreadyBooked)
                throw new DomainException("The court is already booked for the selected time slot.");
            var booking = new Booking(command.CourtId, command.CustomerName, slot);

            await _bookingRepository.AddAsync(booking, cancellationToken);


            return new CreateBookingResult(
                booking.Id,
                booking.CourtId,
                booking.CustomerName,
                booking.Slot.Start,
                booking.Slot.End,
                booking.Status.ToString()
            );




        }

    }
}