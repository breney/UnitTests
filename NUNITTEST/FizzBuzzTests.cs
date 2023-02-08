using TestNinja.Fundamentals;

namespace NUNITTEST;

[TestFixture]
public class FizzBuzzTests
{
    [Test]
    [TestCase(15, "FizzBuzz")]
    [TestCase(9, "Fizz")]
    [TestCase(10, "Buzz")]
    public void GetOutPut_FirstArgumentDivided_ReturnsRightResult(int number, string result)
    {
        Assert.That(() => FizzBuzz.GetOutPut(number), Is.EqualTo(result));
    }

    [Test]
    public void GetOutPut_ArgumentNotDividedBy3and5_ReturnsSameNumberInString()
    {
        Assert.That(() => FizzBuzz.GetOutPut(2), Is.EqualTo("2"));
    }
}