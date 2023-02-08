using TestNinja.Fundamentals;

namespace NUNITTEST;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _customerController;
    [SetUp]
    public void SetUp()
    {
        _customerController = new CustomerController();
    }
    
    [Test]
    public void GetCustomer_IdIsZero_ReturnNotFound()
    {
        var result = _customerController.GetCustomer(0);
        
        //NotFound
        Assert.That(result, Is.TypeOf<CustomerController.NotFound>());
        
        //NotFound or one of its derivates
        //Assert.That(result, Is.InstanceOf<CustomerController.NotFound>());
    }
    
    [Test]
    public void GetCustomer_IdIsOne_ReturnOk()
    {
        var result = _customerController.GetCustomer(1);
        
        Assert.That(result, Is.TypeOf<CustomerController.Ok>());
    }
}