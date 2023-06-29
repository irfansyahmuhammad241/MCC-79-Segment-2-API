using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingDbContext context) : base(context)
        {
        }

        public ICollection<Booking> GetBookingLength()
        {
            return _context.Set<Booking>().ToList();
        }
    }
}
