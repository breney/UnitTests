using Microsoft.AspNetCore.Mvc;
using Moq;
using TestNinja.Mocking;

namespace NUNITTEST.Mocking;

[TestFixture]
public class BookingHelperTests
{
    private Mock<IBookingController> _bookingController;
    private BookingHelper.Booking _booking;

    [SetUp]
    public void SetUp()
    {
        _bookingController = new Mock<IBookingController>();
        _booking = new BookingHelper.Booking()
        {
            Id = 2,
            ArrivalDate = ArriveOn(2017,1,15),
            DepartureDate = DepartOn(2017,1,20),
            Reference = "a"
        };
    }
    [Test]
    public void OverLappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        _bookingController.Setup(bc => bc.GetActiveBooks(2)).Returns(new List<BookingHelper.Booking>{_booking}.AsQueryable());

        var result = BookingHelper.OverLappingBookingsExist(new BookingHelper.Booking()
        { 
            Id = 1,
            ArrivalDate = Before(_booking.ArrivalDate),
            DepartureDate = Before(_booking.DepartureDate)
        }, _bookingController.Object);
        
        Assert.That(result, Is.Empty);
    }
    
    //[Test]
    //[Ignore("Cause i wanted too")]
    //public void OverLappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddle_ReturnEmptyString()
    //{
    //    _bookingController.Setup(bc => bc.GetActiveBooks(1)).Returns(new List<BookingHelper.Booking>{_booking}.AsQueryable());
    //
    //    var result = BookingHelper.OverLappingBookingsExist(new BookingHelper.Booking()
    //    { 
    //        Id = 1,
    //        ArrivalDate = Before(_booking.ArrivalDate),
    //        DepartureDate = After(_booking.DepartureDate)
    //    }, _bookingController.Object);
    //    
    //    Assert.That(result, Is.EqualTo(_booking.Reference));
    //}
    
    [Test]
    public void OverLappingBookingsExist_BookingIsCancelled_ReturnEmptyString()
    {
        _bookingController.Setup(bc => bc.GetActiveBooks(1)).Returns(new List<BookingHelper.Booking>{_booking}.AsQueryable());

        var result = BookingHelper.OverLappingBookingsExist(new BookingHelper.Booking()
        { 
            Id = 1,
            Status = "Cancelled",
            ArrivalDate = Before(_booking.ArrivalDate),
            DepartureDate = Before(_booking.DepartureDate)
        }, _bookingController.Object);
        
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    private DateTime Before(DateTime data)
    {
        return data.AddDays(-1);
    }
    
    private DateTime After(DateTime data)
    {
        return data.AddDays(1);
    }

    private DateTime ArriveOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }
    
    private DateTime DepartOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }
}