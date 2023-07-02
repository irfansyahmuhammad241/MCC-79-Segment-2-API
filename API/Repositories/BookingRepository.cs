using API.Contracts;
using API.Data;
using API.DTOS.Bookings;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingDbContext context) : base(context)
        {
        }

        public IEnumerable<BookingDetailsDto> GetBookingDetails()
        {
            var bookings = _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Employee)
                .Where(b => b.Room.Guid == b.Room.Guid && b.Employee.Guid == b.Employee.Guid)
                .ToList();

            var bookingDetails = bookings.Select(b => new BookingDetailsDto
            {
                Guid = b.Guid,
                BookedNIK = b.Employee.NIK,
                BookedBye = b.Employee.FirstName + " " + b.Employee.LastName,
                RoomName = b.Room.Name,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                StatusLevel = b.Status,
                Remarks = b.Remarks,
            });

            return bookingDetails;
        }

    }
}
