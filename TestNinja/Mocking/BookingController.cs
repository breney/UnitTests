namespace TestNinja.Mocking;

using static Mocking.BookingHelper;

public interface IBookingController
{
    IQueryable<Booking> GetActiveBooks(int? id = null);
    Booking getFirstBook(IQueryable<BookingHelper.Booking> books, Booking book);
}

public class BookingController : IBookingController
{
    public IQueryable<Booking> GetActiveBooks(int? id = null)
    {
        var unitOfWork = new UnitOfWork();
        var books = unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");;
        
        if(id.HasValue)
            books = books.Where(b => b.Id != id);
        
        return books;
    }
    
    public Booking getFirstBook(IQueryable<BookingHelper.Booking> books, Booking book)
    {
        var overLappingBooking = books.FirstOrDefault(
            b =>
                book.ArrivalDate >= b.ArrivalDate
                && book.ArrivalDate < b.DepartureDate
                || book.DepartureDate > b.ArrivalDate
                && book.DepartureDate <= b.DepartureDate); 

        return overLappingBooking;
    }
}