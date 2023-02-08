using System.Linq.Expressions;
using TestNinja.Fundamentals;

namespace NUNITTEST;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator _demeritPointsCalculator;
    
    [SetUp]
    public void SetUp()
    {
        _demeritPointsCalculator = new DemeritPointsCalculator();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_ArgumentIsOutOfRange_ReturnOutOfRangeException(int speed)
    {
        Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }
    
    [Test]
    [TestCase(0, 0)]
    [TestCase(50, 0)]
    [TestCase(75,2)]
    public void CalculateDemeritPoints_ArgumentWithSpeed_ReturnDemeritPoints(int number, int result)
    {
        Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(number), Is.EqualTo(result));
    }
}