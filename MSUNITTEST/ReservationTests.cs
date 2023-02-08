using TestNinja.Fundamentals;

namespace MSUNITTEST;

// MsUnitTest

[TestClass]
public class ReservationTests
{
    [TestMethod]
    public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
    {
        // Arrange
        var reservation = new Reservation();

        // Act
        var result = reservation.CanBeCancelledBy(new Reservation.User { IsAdmin = true });

        // Assert
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void CanBeCancelledBy_MadeByEqualUser_ReturnsTrue()
    {
        // Arrange
        var user = new Reservation.User();
        var reservation = new Reservation{ MadeBy = user };

        // Act
        var result = reservation.CanBeCancelledBy(user);

        // Assert
        Assert.IsTrue(result);
    }
   
    [TestMethod]
    public void CanBeCancelledBy_ReturnsFalse()
    {
        // Arrange
        var reservation = new Reservation{MadeBy = new Reservation.User()};

        // Act
        var result = reservation.CanBeCancelledBy(new Reservation.User());

        // Assert
        Assert.IsFalse(result);
    }
}