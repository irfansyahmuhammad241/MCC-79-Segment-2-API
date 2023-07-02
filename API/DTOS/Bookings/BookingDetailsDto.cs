using API.Utilities.Enums;

namespace API.DTOS.Bookings
{
    public class BookingDetailsDto
    {
        public Guid Guid { get; set; }
        public string BookedNIK { get; set; }
        public string BookedBye { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel StatusLevel { get; set; }
        public string Remarks { get; set; }
    }
}
