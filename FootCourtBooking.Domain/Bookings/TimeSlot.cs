using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Domain.Common;

namespace FootCourtBooking.Domain.Bookings
{
    public sealed class TimeSlot
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public TimeSlot()
        {

        }
        public TimeSlot(DateTime start, DateTime end)
        {
            if (end <= start)

                throw new DomainException("End time must be greater than start time");


            if (start.Minute != 0 || start.Second != 0 || start.Millisecond != 0)
                throw new DomainException("Start time must be on the hour");

            if (end.Minute != 0 || end.Second != 0 || end.Millisecond != 0)
                throw new DomainException("End time must be on the hour");

            if ((end - start).TotalHours != 1)
                throw new DomainException("Booking durration must be exactly 1 hour");
            Start = start;
            End = end;

        }

        public bool Overlaps(TimeSlot other)
        {
            return Start < other.End && End > other.Start;
        }

    }
}