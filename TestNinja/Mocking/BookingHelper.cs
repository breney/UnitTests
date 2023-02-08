namespace TestNinja.Mocking;

public static class BookingHelper
{
    public static string OverLappingBookingsExist(Booking booking, IBookingController bookingController)
    {
        if (booking.Status == "Cancelled")
            return string.Empty;

        var books = bookingController.GetActiveBooks(booking.Id);

        var overLappingBooking = bookingController.getFirstBook(books, booking);

        return overLappingBooking == null ? string.Empty : overLappingBooking.Reference;
    }
    
    public interface IUnitOfWork
    {
        public IQueryable<T> Query<T>();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
        
    }
    
}