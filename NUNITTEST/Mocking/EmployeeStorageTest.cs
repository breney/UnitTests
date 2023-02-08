using Moq;
using TestNinja.Mocking;

namespace NUNITTEST.Mocking;

[TestFixture]
public class EmployeeStorageTest
{
    private Mock<IEmployeeStorage> _employeeStorage;
    private EmployeeController _employeeController;
    
    [SetUp]
    public void SetUp()
    {
        _employeeStorage = new Mock<IEmployeeStorage>();
        _employeeController = new EmployeeController(_employeeStorage.Object);
    }

    [Test]
    public void DeleteEmployee_WhenUser_RedirectResult()
    {
        _employeeController.DeleteEmployee(1);
        
        _employeeStorage.Verify(s => s.DeleteEmployee(1));
    }
}