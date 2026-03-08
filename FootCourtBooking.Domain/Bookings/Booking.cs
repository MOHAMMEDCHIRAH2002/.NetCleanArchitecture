using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootCourtBooking.Domain.Common;

namespace FootCourtBooking.Domain.Bookings
{
    public class Booking
    {
        public Guid Id { get; private set; }
        public Guid CourtId { get; private set; }

        public string CustomerName { get; private set; } = default!;

        public TimeSlot Slot { get; private set; } = default!;
        public BoockingStatus Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public Booking()
        {

        }

        public Booking(Guid courtId, string customerName, TimeSlot slot)
        {

            if (courtId == Guid.Empty)
                throw new DomainException("CourtId is required");

            if (string.IsNullOrWhiteSpace(customerName))
                throw new DomainException("CustomerName is required");

            Id = Guid.NewGuid();
            CourtId = courtId;
            CustomerName = customerName;
            Slot = slot;
            Status = BoockingStatus.Pending;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public static Booking Create(Guid courtId, string customerName, TimeSlot slot)
        {
            return new Booking(courtId, customerName, slot);
        }

        public void Confirm()
        {
            if (Status == BoockingStatus.Canceled)
                throw new DomainException("Canceled booking coannot be confirmed");
            if (Status == BoockingStatus.Confirmed)
                return;

            Status = BoockingStatus.Confirmed;

        }

        public void Cancel(DateTime nowutc)
        {
            if (nowutc >= Slot.End)
                throw new DomainException("Booking cannot be canceled after is has started .");
            if (Status == BoockingStatus.Canceled)
                return;

            Status = BoockingStatus.Canceled;
        }

        public bool IsCanceled() => Status == BoockingStatus.Canceled;
    }

}