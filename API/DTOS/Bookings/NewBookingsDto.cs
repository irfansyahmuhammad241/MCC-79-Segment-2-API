using API.Utilities.Enums;

namespace API.DTOS.Bookings
{
    public class NewBookingsDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StatusLevel Status { get; set; }

        public string Remarks { get; set; }

        public Guid RoomGuid { get; set; }

        public Guid EmployeeGuid { get; set; }
    }
}
