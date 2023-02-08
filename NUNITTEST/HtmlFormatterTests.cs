using TestNinja.Fundamentals;

namespace NUNITTEST;
[TestFixture]
public class HtmlFormatterTests
{
    private HtmlFormatter _htmlFormatter;
    
    [SetUp]
    public void SetUp()
    {
        _htmlFormatter = new HtmlFormatter();
    }
    
    [Test]
    public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
    {
        var result = _htmlFormatter.FormatAsBold("abc");
        
        //Specific
        Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
        
        //More general
        Assert.That(result, Does.StartWith("<strong>"));
        Assert.That(result, Does.EndWith("</strong>"));
        Assert.That(result, Does.Contain("abc"));
    }
}